using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PythonTools.Django.TemplateParsing.DjangoBlocks {
    class DjangoIfBlock : DjangoBlock {
        public readonly BlockClassification[] Args;

        public DjangoIfBlock(BlockParseInfo parseInfo, params BlockClassification[] args)
            : base(parseInfo) {
            Args = args;
        }

        public static DjangoBlock Parse(BlockParseInfo parseInfo) {
            var words = parseInfo.Args.Split(' ');
            List<BlockClassification> argClassifications = new List<BlockClassification>();

            int wordStart = parseInfo.Start + parseInfo.Command.Length;
            foreach (var word in words) {
                bool hasNewline = false;
                if (word.Contains('\r') || word.Contains('\n')) {
                    hasNewline = true;
                    if (word.Trim().Length == 0) {
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(word)) {
                    Classification curKind;
                    switch (word) {
                        case "and":
                        case "or":
                        case "not":
                            curKind = Classification.Keyword;
                            break;
                        default:
                            curKind = Classification.Identifier;
                            break;
                    }

                    argClassifications.Add(
                        new BlockClassification(
                            new Span(wordStart, word.Length),
                            curKind
                        )
                    );
                }

                if (hasNewline) {
                    break;
                }

                wordStart += word.Length + 1;
            }

            return new DjangoIfBlock(parseInfo, argClassifications.ToArray());
        }

        public override IEnumerable<BlockClassification> GetSpans() {
            yield return new BlockClassification(new Span(ParseInfo.Start, ParseInfo.Command.Length), Classification.Keyword);
            foreach (var arg in Args) {
                yield return arg;
            }
        }

        public override IEnumerable<CompletionInfo> GetCompletions(IDjangoCompletionContext context, int position) {
            // no argument yet, or the last argument was a keyword, then we are completing an identifier
            if (Args.Length == 0 || Args.Last().Classification == Classification.Keyword || position <= Args.Last().Span.End) {
                // get the variables
                return Enumerable.Concat(
                    base.GetCompletions(context, position),
                    new[] {
                        new CompletionInfo("not", StandardGlyphGroup.GlyphKeyword)
                    }
                );
            } else {
                // last word was an identifier, so we'll complete and/or
                return new[] {
                    new CompletionInfo("and", StandardGlyphGroup.GlyphKeyword),
                    new CompletionInfo("or", StandardGlyphGroup.GlyphKeyword)
                };
            }
        }
    }
}
