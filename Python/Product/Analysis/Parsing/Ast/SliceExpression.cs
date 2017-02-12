using System.Text;

namespace Microsoft.PythonTools.Parsing.Ast {
    public class SliceExpression : Expression {
        private readonly Expression _sliceStart;
        private readonly Expression _sliceStop;
        private readonly Expression _sliceStep;
        private readonly bool _stepProvided;

        public SliceExpression(Expression start, Expression stop, Expression step, bool stepProvided) {
            _sliceStart = start;
            _sliceStop = stop;
            _sliceStep = step;
            _stepProvided = stepProvided;
        }

        public Expression SliceStart {
            get { return _sliceStart; }
        }

        public Expression SliceStop {
            get { return _sliceStop; }
        }

        public Expression SliceStep {
            get { return _sliceStep; }
        }

        /// <summary>
        /// True if the user provided a step parameter (either providing an explicit parameter
        /// or providing an empty step parameter) false if only start and stop were provided.
        /// </summary>
        public bool StepProvided {
            get {
                return _stepProvided;
            }
        }

        public override void Walk(PythonWalker walker) {
            if (walker.Walk(this)) {
                if (_sliceStart != null) {
                    _sliceStart.Walk(walker);
                }
                if (_sliceStop != null) {
                    _sliceStop.Walk(walker);
                }
                if (_sliceStep != null) {
                    _sliceStep.Walk(walker);
                }
            }
            walker.PostWalk(this);
        }

        internal override void AppendCodeString(StringBuilder res, PythonAst ast, CodeFormattingOptions format) {
            if (_sliceStart != null) {
                _sliceStart.AppendCodeString(res, ast, format);
            }
            if (!this.IsIncompleteNode(ast)) {
                res.Append(this.GetProceedingWhiteSpace(ast));
                res.Append(':');
                if (_sliceStop != null) {
                    _sliceStop.AppendCodeString(res, ast, format);
                }
                if (_stepProvided) {
                    res.Append(this.GetSecondWhiteSpace(ast));
                    res.Append(':');
                    if (_sliceStep != null) {
                        _sliceStep.AppendCodeString(res, ast, format);
                    }
                }
            }
        }


        public override string GetLeadingWhiteSpace(PythonAst ast) {
            if (_sliceStart != null) {
                return _sliceStart.GetLeadingWhiteSpace(ast);
            }
            return this.GetProceedingWhiteSpace(ast);
        }

        public override void SetLeadingWhiteSpace(PythonAst ast, string whiteSpace) {
            if (_sliceStart != null) {
                _sliceStart.SetLeadingWhiteSpace(ast, whiteSpace);
            } else {
                base.SetLeadingWhiteSpace(ast, whiteSpace);
            }
        }

    }
}
