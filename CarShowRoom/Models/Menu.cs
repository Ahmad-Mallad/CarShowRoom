using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Models
{
	public class Menu:SharedProp
	{
		public int MenuId { get; set; }
		public string MenuTitle { get; set; }
		public string MenuUrl { get; set; }

		[ForeignKey(nameof(ParentMenu))]
		public int? ParentId { get; set; } // Nullable for top-level menus

		public Menu ?ParentMenu { get; set; }
		public ICollection<Menu>? SubMenus { get; set; }
	}

}
