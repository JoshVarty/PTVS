
using System.Windows.Input;
namespace TestUtilities {
    public interface ITreeNode {
        void Select();
        void AddToSelection();

        void DragOntoThis(params ITreeNode[] source);
        void DragOntoThis(Key modifier, params ITreeNode[] source);
    }
}
