namespace Microsoft.CookiecutterTools.ViewModel {
    class ContinuationViewModel {
        public ContinuationViewModel() :
            this(null) {
        }

        public ContinuationViewModel(string continuationToken) {
            ContinuationToken = continuationToken;
        }

        public bool Selectable => false;

        public string ContinuationToken { get; set; }
    }
}
