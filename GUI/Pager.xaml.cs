using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI;

public partial class Pager : UserControl {
  public Pager() {
    InitializeComponent();
    Loaded += Pager_Loaded;
  }

  private void Pager_Loaded(object sender, RoutedEventArgs e) {
    UpdatePageIndicator();
  }

  public List<PageItem> Pages {
    get { return (List<PageItem>)GetValue(PagesProperty); }
    set { SetValue(PagesProperty, value); }
  }

  public static readonly DependencyProperty PagesProperty =
      DependencyProperty.Register(
          "Pages",
          typeof(List<PageItem>),
          typeof(Pager),
          new PropertyMetadata(null, OnPagesChanged)
      );

  private static void OnPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
    var control = (Pager)d;
    control.ListBox.ItemsSource = control.Pages;
    control.UpdatePageIndicator();
  }

  private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
    UpdatePageIndicator();
    var selectedItem = ListBox.SelectedItem as PageItem;
    if (selectedItem != null) {
      PageName.Text = selectedItem.Name;
    }
  }

  private void UpdatePageIndicator() {
    var selectedIndex  = ListBox.SelectedIndex;
    var indicatorItems = new List<Brush>();

    for (int i = 0; i < ListBox.Items.Count; i++) {
      indicatorItems.Add(i == selectedIndex ? Brushes.Black : Brushes.Gray);
    }

    PageIndicator.ItemsSource = indicatorItems;
  }

  public class PageItem {
    public string    Name    { get; set; }
    public UIElement Content { get; set; }
  }
}
