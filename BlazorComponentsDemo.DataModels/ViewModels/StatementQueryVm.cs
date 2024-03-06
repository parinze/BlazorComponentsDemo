using BlazorComponentsDemo.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponentsDemo.DataModels.ViewModels
{
	public class StatementQueryVm
	{
		public bool Ok { get; set; }
		public virtual IEnumerable<StatementQueryDto> Data { get; set; }
	}
}
