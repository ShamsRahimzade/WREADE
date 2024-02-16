﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Application.ViewModels.Setting
{
	public class CreateSettingVM
	{
		[Required]
		[MaxLength(100)]
		public string Key { get; set; }
		[Required]
		[MaxLength(100)]
		public string Value { get; set; }
	}
}
