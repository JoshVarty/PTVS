using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudioTools.Navigation {
    /// <summary>
    /// Class used to identify a module. The module is identified using the hierarchy that
    /// contains it and its item id inside the hierarchy.
    /// </summary>
    public sealed class ModuleId {
        private IVsHierarchy _ownerHierarchy;
        private uint _itemId;

        public ModuleId(IVsHierarchy owner, uint id) {
            _ownerHierarchy = owner;
            _itemId = id;
        }

        public IVsHierarchy Hierarchy {
            get { return _ownerHierarchy; }
        }

        public uint ItemID {
            get { return _itemId; }
        }

        public override int GetHashCode() {
            int hash = 0;
            if (null != _ownerHierarchy) {
                hash = _ownerHierarchy.GetHashCode();
            }
            hash = hash ^ (int)_itemId;
            return hash;
        }

        public override bool Equals(object obj) {
            ModuleId other = obj as ModuleId;
            if (null == obj) {
                return false;
            }
            if (!_ownerHierarchy.Equals(other._ownerHierarchy)) {
                return false;
            }
            return (_itemId == other._itemId);
        }
    }
}