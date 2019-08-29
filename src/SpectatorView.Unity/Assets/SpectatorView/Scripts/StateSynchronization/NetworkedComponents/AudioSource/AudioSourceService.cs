﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Microsoft.MixedReality.SpectatorView
{
    internal class AudioSourceService : ComponentBroadcasterService<AudioSourceService, AudioSourceObserver>, IAssetCacheUpdater
    {
        public static readonly ShortID ID = new ShortID("AUD");

        public override ShortID GetID() { return ID; }

        private const int DSPBufferSize = 1024;
        private const AudioSpeakerMode SpeakerMode = AudioSpeakerMode.Stereo;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            StateSynchronizationSceneManager.Instance.RegisterService(this, new ComponentBroadcasterDefinition<AudioSourceBroadcaster>(typeof(AudioSource)));
        }

        public Guid GetAudioClipId(AudioClip clip)
        {
            var audioClipAssets = AudioClipAssetCache.Instance;
            if (audioClipAssets == null)
            {
                return Guid.Empty;
            }
            else
            {
                return audioClipAssets.GetAssetId(clip);
            }
        }

        public AudioClip GetAudioClip(Guid assetId)
        {
            var audioClipAssets = AudioClipAssetCache.Instance;

            if (audioClipAssets == null)
            {
                return null;
            }
            else
            {
                return audioClipAssets.GetAsset(assetId);
            }
        }

        public Guid GetAudioMixerGroupId(AudioMixerGroup group)
        {
            var audioMixerGroups = AudioMixerGroupAssetCache.Instance;
            if (audioMixerGroups == null)
            {
                return Guid.Empty;
            }
            else
            {
                return audioMixerGroups.GetAssetId(group);
            }
        }

        public AudioMixerGroup GetAudioMixerGroup(Guid assetId)
        {
            var audioMixerGroups = AudioMixerGroupAssetCache.Instance;
            if (audioMixerGroups == null)
            {
                return null;
            }
            else
            {
                return audioMixerGroups.GetAsset(assetId);
            }
        }

        public void UpdateAssetCache()
        {
            AudioClipAssetCache.GetOrCreateAssetCache<AudioClipAssetCache>().UpdateAssetCache();
            AudioMixerGroupAssetCache.GetOrCreateAssetCache<AudioMixerGroupAssetCache>().UpdateAssetCache();
        }

        public void ClearAssetCache()
        {
            AudioClipAssetCache.GetOrCreateAssetCache<AudioClipAssetCache>().ClearAssetCache();
            AudioMixerGroupAssetCache.GetOrCreateAssetCache<AudioMixerGroupAssetCache>().ClearAssetCache();
        }
    }
}