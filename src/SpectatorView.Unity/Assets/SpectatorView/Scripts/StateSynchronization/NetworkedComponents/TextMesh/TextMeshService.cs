﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Microsoft.MixedReality.SpectatorView
{
    internal class TextMeshService : ComponentBroadcasterService<TextMeshService, TextMeshObserver>, IAssetCacheUpdater
    {
        public static readonly ShortID ID = new ShortID("TXT");

        public override ShortID GetID() { return ID; }

        private void Start()
        {
            StateSynchronizationSceneManager.Instance.RegisterService(this, new ComponentBroadcasterDefinition<TextMeshBroadcaster>(typeof(TextMesh), typeof(MeshRenderer)));
        }

        public Guid GetFontId(Font font)
        {
            var fontAssets = FontAssetCache.Instance;
            if (fontAssets == null)
            {
                return Guid.Empty;
            }
            else
            {
                return fontAssets.GetAssetId(font);
            }
        }

        public Font GetFont(Guid guid)
        {
            var fontAssets = FontAssetCache.Instance;
            if (fontAssets == null)
            {
                return null;
            }
            else
            {
                return fontAssets.GetAsset(guid);
            }
        }

        public void UpdateAssetCache()
        {
            FontAssetCache.GetOrCreateAssetCache<FontAssetCache>().UpdateAssetCache();
        }

        public void ClearAssetCache()
        {
            FontAssetCache.GetOrCreateAssetCache<FontAssetCache>().ClearAssetCache();
        }
    }
}
