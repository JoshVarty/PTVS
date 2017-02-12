using Microsoft.PythonTools.Django.Analysis;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoUrlBlock : DjangoBlock {
        public readonly BlockClassification[] Args;
        private readonly string _urlName;
        private readonly string[] _definedNamedParameters;
        private readonly uint _nbDefinedParameters;

        public DjangoUrlBlock(BlockParseInfo parseInfo, BlockClassification[] args, string urlName = null, string[] definedNamedParameters = null, uint nbDefinedParameters = 0)
            : base(parseInfo) {
            Args = args;
            _urlName = urlName;
            _definedNamedParameters = definedNamedParameters != null ? definedNamedParameters : Array.Empty<string>();
            _nbDefinedParameters = nbDefinedParameters;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            string[] words = parseInfo.Args.Split(' ');
            IList<BlockClassification> argClassifications = new List<BlockClassification>();
            IList<string> usedNamedParameters = new List<string>();
            uint nbParameters = 0;
            bool afterUrl = false, beforeAsKeyword = true;

            int wordStart = parseInfo.Start + parseInfo.Command.Length;
            string urlName = null;
            foreach (string word in words) {
                if (!string.IsNullOrEmpty(word)) {
                    Classification currentArgKind;
                    if (word.Equals("as")) {
                        currentArgKind = Classification.Keyword;
                        beforeAsKeyword = false;
                    } else {
                        currentArgKind = Classification.Identifier;
                    }
                    argClassifications.Add(new BlockClassification(new Span(wordStart, word.Length), currentArgKind));

                    if (afterUrl && beforeAsKeyword) {
                        ++nbParameters;
                    }

                    // Get url name
                    if (urlName == null) {
                        if (word.StartsWith("'")) {
                            urlName = word.TrimStart('\'').TrimEnd('\'');
                            afterUrl = true;
                        } else if (word.StartsWith("\"")) {
                            urlName = word.TrimStart('"').TrimEnd('"');
                            afterUrl = true;
                        }
                    }

                    // Test if word is a named parameter (but not in a string)
                    if (word.Contains('=') && !word.StartsWith("'") && !word.StartsWith("\"")) {
                        usedNamedParameters.Add(word.Split('=').First());
                    }
                }
                wordStart += word.Length + 1;
            }

            return new DjangoUrlBlock(parseInfo, argClassifications.ToArray(), urlName, usedNamedParameters.ToArray(), nbParameters);
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            yield return new BlockClassification(new Span(ParseInfo.Start, ParseInfo.Command.Length), Classification.Keyword);
            foreach (var arg in Args) {
                yield return arg;
            }
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            // If no argument, show urls
            if (Args.Length == 0) {
                return GetUrlCompletion(context);
            }

            BlockClassification? argBeforePosition = GetArgBeforePosition(position);
            BlockClassification? argPenultimateBeforePosition = GetPenultimateArgBeforePosition(position);
            if (argBeforePosition == null)
                return GetUrlCompletion(context);

            // No completion for the url variable name after the 'as' keyword
            if (IsAfterAsKeyword(argBeforePosition, argPenultimateBeforePosition))
                return Enumerable.Empty<CompletionInfo>();

            DjangoUrl url = FindCurrentDjangoUrl(context);
            // Too many parameters are already in the statement
            if (_nbDefinedParameters >= url.Parameters.Count) {
                return new[] {
                    new CompletionInfo("as", StandardGlyphGroup.GlyphKeyword),
                };
            }

            // Completion proposes unused named parameters, template variables and the 'as' keyword
            return Enumerable.Concat(
                Enumerable.Concat(
                    GetUnusedNamedParameters(url),
                    base.GetCompletions(context, position)
                ),
                new[] {
                    new CompletionInfo("as", StandardGlyphGroup.GlyphKeyword)
                }
            );
        }

        private IEnumerable<CompletionInfo> GetUrlCompletion(IDjangoCompletionContext context) {
            return CompletionInfo.ToCompletionInfo(context.Urls.Select(url => "'{0}'".FormatInvariant(url.FullName)), StandardGlyphGroup.GlyphGroupField);
        }

        private BlockClassification? GetArgBeforePosition(int position) {
            BlockClassification? argBeforePosition = null;
            foreach (BlockClassification arg in Args) {
                if (position > arg.Span.Start) {
                    argBeforePosition = arg;
                } else {
                    break;
                }
            }

            return argBeforePosition;
        }

        private BlockClassification? GetPenultimateArgBeforePosition(int position) {
            if (Args.Length < 2) {
                return null;
            }

            BlockClassification? argBeforePosition = null;
            BlockClassification? argPenultimateBeforePosition = null;
            foreach (BlockClassification arg in Args) {
                if (position > arg.Span.Start) {
                    argPenultimateBeforePosition = argBeforePosition;
                    argBeforePosition = arg;
                } else {
                    break;
                }
            }

            return argPenultimateBeforePosition;
        }

        private bool IsAfterAsKeyword(BlockClassification? argBeforePosition, BlockClassification? argPenultimateBeforePosition) {
            return argBeforePosition.Value.Classification == Classification.Keyword || (argPenultimateBeforePosition != null && argPenultimateBeforePosition.Value.Classification == Classification.Keyword);
        }

        private DjangoUrl FindCurrentDjangoUrl(IDjangoCompletionContext context) {
            return context.Urls.Where(url => url.FullName.Equals(_urlName)).FirstOrDefault();
        }

        private IEnumerable<CompletionInfo> GetUnusedNamedParameters(DjangoUrl url) {
            if (url == null) {
                return Enumerable.Empty<CompletionInfo>();
            }

            IList<string> undefinedNamedParameters = new List<string>();
            foreach (DjangoUrlParameter urlParam in url.NamedParameters) {
                if (!_definedNamedParameters.Contains(urlParam.Name)) {
                    undefinedNamedParameters.Add(urlParam.Name);
                }
            }

            return CompletionInfo.ToCompletionInfo(undefinedNamedParameters.Select(p => p += '='), StandardGlyphGroup.GlyphGroupField);
        }
    }
}
