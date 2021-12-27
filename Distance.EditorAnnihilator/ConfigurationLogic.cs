using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reactor.API.Configuration;
using System;
using UnityEngine;

namespace Mod.EditorAnnihilator
{
	public class ConfigurationLogic : MonoBehaviour
	{
		#region Properties
		public bool ModeReq
		{
			get => Get<bool>("ModeReq");
			set => Set("ModeReq", value);
		}
		public bool AnimUnbounded
		{
			get => Get<bool>("AnimUnbounded");
			set => Set("AnimUnbounded", value);
		}
		public bool SNMS
		{
			get => Get<bool>("SNMS");
			set => Set("SNMS", value);
		}

		public int edp
		{
			get => Get<int>("edp");
			set => Set("edp", value);
		}

		public bool ccwbl
		{
			get => Get<bool>("ccwbl");
			set => Set("ccwbl", value);
		}
		public bool dpmt1c
		{
			get => Get<bool>("dpmt1c");
			set => Set("dpmt1c", value);
		}
		public bool hf
		{
			get => Get<bool>("hf");
			set => Set("hf", value);
		}
		public bool em
		{
			get => Get<bool>("em");
			set => Set("em", value);
		}
		public bool voic
		{
			get => Get<bool>("voic");
			set => Set("voic", value);
		}
		public bool cnf
		{
			get => Get<bool>("cnf");
			set => Set("cnf", value);
		}
		#endregion

		internal Settings Config;

		public event Action<ConfigurationLogic> OnChanged;

		private void Load()
		{
			Config = new Settings("Config");
		}

		public void Awake()
		{
			Load();

			Get("ModeReq", true);
			Get("AnimUnbounded", false);
			Get("SNMS", false);
			Get("edp", 3);
			Get("ccwbl", true);
			Get("dpmt1c", true);
			Get("hf", true);
			Get("em", false);
			Get("voic", true);
			Get("cnf", true);
			Save();
		}

		public T Get<T>(string key, T @default = default)
		{
			return Config.GetOrCreate(key, @default);
		}

		public void Set<T>(string key, T value)
		{
			Config[key] = value;
			Save();
		}

		public void Save()
		{
			Config?.Save();
			OnChanged?.Invoke(this);
		}
	}
}