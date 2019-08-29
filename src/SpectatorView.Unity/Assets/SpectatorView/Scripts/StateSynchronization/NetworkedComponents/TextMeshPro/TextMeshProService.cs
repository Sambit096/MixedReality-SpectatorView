﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#if STATESYNC_TEXTMESHPRO
using System;
using TMPro;
#endif

namespace Microsoft.MixedReality.SpectatorView
{
    internal class TextMeshProService : ComponentBroadcasterService<TextMeshProService, TextMeshProObserver>, IAssetCacheUpdater
    {
        public static readonly ShortID ID = new ShortID("TMP");

        public override ShortID GetID() { return ID; }

#if STATESYNC_TEXTMESHPRO
        private void Start()
        {
            StateSynchronizationSceneManager.Instance.RegisterService(this, new ComponentBroadcasterDefinition<TextMeshProBroadcaster>(typeof(TextMeshPro)));
        }

        public Guid GetFontId(TMP_FontAsset font)
        {
            var fontAssets = TextMeshProFontAssetCache.Instance;
            if (fontAssets == null)
            {
                return Guid.Empty;
            }
            else
            {
                return fontAssets.GetAssetId(font);
            }
        }

        public TMP_FontAsset GetFont(Guid guid)
        {
            var fontAssets = TextMeshProFontAssetCache.Instance;
            if (fontAssets == null)
            {
                return null;
            }
            else
            {
                return (TMP_FontAsset)fontAssets.GetAsset(guid);
            }
        }
#endif

        public void UpdateAssetCache()
        {
            TextMeshProFontAssetCache.GetOrCreateAssetCache<TextMeshProFontAssetCache>().UpdateAssetCache();
        }

        public void ClearAssetCache()
        {
            TextMeshProFontAssetCache.GetOrCreateAssetCache<TextMeshProFontAssetCache>().ClearAssetCache();
        }
    }
}
