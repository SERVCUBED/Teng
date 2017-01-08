using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;

namespace gui
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
        }

        CompletionWindow completionWindow;

        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                // Open code completion after the user has pressed dot:
                completionWindow = new CompletionWindow(textEditor.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                data.Add(new MyCompletionData("{{t foreach page /.+/}}", "Beginning for foreach for all pages matching Regex /.+/"));
                data.Add(new MyCompletionData("{{t endforeach}}", "Marks the end of a foreach."));
                data.Add(new MyCompletionData("{{t page.", "e.g. {{t page.title}} Insert a page part."));
                data.Add(new MyCompletionData("{{t a.", "e.g. {{t a.index}} Insert a formatted url to the page."));
                completionWindow.Show();
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            }
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                    e.Handled = true;
                }
            }
        }

        private void cutBtn_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Cut();
        }

        private void copyBtn_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Copy();
        }

        private void pasteBtn_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Paste();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Delete();
        }

        private void undoBtn_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Undo();
        }

        private void redoBtn_Click(object sender, RoutedEventArgs e)
        {
            textEditor.Redo();
        }

        private void wordWrapChk_Checked(object sender, RoutedEventArgs e)
        {
            if (wordWrapChk.IsChecked != null) textEditor.WordWrap = wordWrapChk.IsChecked.Value;
        }
    }
}
