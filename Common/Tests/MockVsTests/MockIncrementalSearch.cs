using System;
using Microsoft.VisualStudio.Text.IncrementalSearch;
using TestUtilities.Mocks;

namespace Microsoft.VisualStudioTools.MockVsTests {
    class MockIncrementalSearch : IIncrementalSearch {
        private readonly MockTextView _view;
        
        public MockIncrementalSearch(MockTextView textView) {
            _view = textView;
        }

        public IncrementalSearchResult AppendCharAndSearch(char toAppend) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public IncrementalSearchResult DeleteCharAndSearch() {
            throw new NotImplementedException();
        }

        public void Dismiss() {
            throw new NotImplementedException();
        }

        public bool IsActive {
            get { return false; }
        }

        public IncrementalSearchDirection SearchDirection {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public string SearchString {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public IncrementalSearchResult SelectNextResult() {
            throw new NotImplementedException();
        }

        public void Start() {
            throw new NotImplementedException();
        }

        public VisualStudio.Text.Editor.ITextView TextView {
            get { throw new NotImplementedException(); }
        }
    }
}
