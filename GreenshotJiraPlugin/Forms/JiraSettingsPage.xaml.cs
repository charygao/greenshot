﻿
/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2013  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Windows;
using GreenshotPlugin.Core.Settings;
using Greenshot.IniFile;
using GreenshotPlugin.Core;
using System.Collections.Generic;
using GreenshotJiraPlugin;

namespace GreenshotJiraPlugin {
	/// <summary>
	/// Interaction logic for JiraSettingsPage.xaml
	/// </summary>
	public partial class JiraSettingsPage : SettingsPage {
		private readonly static JiraConfiguration jiraConfiguration = IniConfig.GetIniSection<JiraConfiguration>();

		protected override void Initialize() {
			base.Initialize();
			proxy = new IniProxy(jiraConfiguration);
		}
		public JiraSettingsPage() : base() {
			InitializeComponent();
		}

		public override void Commit() {
			string url = jiraConfiguration.Url;
			base.Commit();
			if (!url.Equals(jiraConfiguration.Url) && JiraPlugin.Instance.CurrentJiraConnector != null && JiraPlugin.Instance.CurrentJiraConnector.isLoggedIn) {
				// Reconnect!
				JiraPlugin.Instance.CurrentJiraConnector.logout();
				JiraPlugin.Instance.CurrentJiraConnector.login();
			}
		}
	}
}