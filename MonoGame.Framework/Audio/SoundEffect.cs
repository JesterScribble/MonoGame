#region License
/*
Microsoft Public License (Ms-PL)
MonoGame - Copyright © 2009 The MonoGame Team

All rights reserved.

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not
accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under 
U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
your patent license from such contributor to the software ends automatically.
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
notices that are present in the software.
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including 
a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object 
code form, you may only do so under a license that complies with this license.
(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees
or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent
permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular
purpose and non-infringement.
*/
#endregion License
﻿
using System;
using System.IO;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Microsoft.Xna.Framework.Audio
{
    public sealed class SoundEffect : IDisposable
    {
		private static Sound _sound;
		private string _name = "";
		
		internal SoundEffect(string fileName)
		{
			_name = fileName;		
			
			if (_name == string.Empty )
			{
			  throw new FileNotFoundException("Supported Sound Effect formats are wav, mp3, acc, aiff");
			}
			
			_sound = new Sound(_name, 1.0f, false);
		}
		
        public bool Play()
        {				
			return Play(MasterVolume, 0.0f, 0.0f);
        }

        public bool Play(float volume, float pitch, float pan)
        {
			if ( MasterVolume > 0.0f )
			{
				SoundEffectInstance instance = CreateInstance();
				instance.Volume = volume;
				instance.Pitch = pitch;
				instance.Pan = pan;
				instance.Play();
				return instance.Sound.Playing;
			}
			return false;
        }
		
		public TimeSpan Duration 
		{ 
			get
			{
				if ( _sound != null )
				{
					return new TimeSpan(0,0,(int)_sound.Duration);
				}
				else
				{
					return new TimeSpan(0);
				}
			}
		}

        public string Name
        {
            get
            {
				return Path.GetFileNameWithoutExtension(_name);
            }
        }
		
		public SoundEffectInstance CreateInstance ()
		{
			var instance = new SoundEffectInstance();
			_sound = new Sound( _name, MasterVolume, false);
			instance.Sound = _sound;
			return instance;
			
		}
		
		#region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
		
		static float _masterVolume = 1.0f;
		public static float MasterVolume 
		{ 
			get
			{
				return _masterVolume;
			}
			set
			{
				_masterVolume = value;	
			}
		}
    }
}

