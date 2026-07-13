using UnityEngine;

namespace LFBetterMusic.Lyrics
{
    /// <summary>
    /// 当前 1163 播放会话内的浮动歌词交互状态。
    /// 背景音乐跨 Talk 时复用；新的播放型 1163 会创建全新状态，恢复 EFFECT 初始参数与默认位置。
    /// </summary>
    public sealed class FloatingLyricsRuntimeState
    {
        public FloatingLyricsRuntimeState(
            int initialSizeMode,
            int initialColorMode,
            bool usesDynamicLineColors)
        {
            InitialSizeMode = NormalizeVisibleSizeMode(initialSizeMode);
            InitialColorMode = Mathf.Clamp(initialColorMode, 0, 12);
            RuntimeSizeMode = InitialSizeMode;
            RuntimeColorMode = InitialColorMode;
            UsesDynamicLineColors = usesDynamicLineColors;
        }

        public int InitialSizeMode { get; }
        public int InitialColorMode { get; }
        public bool UsesDynamicLineColors { get; }

        public int RuntimeSizeMode { get; private set; }
        public int RuntimeColorMode { get; private set; }
        public bool HasSizeOverride { get; private set; }
        public bool HasColorOverride { get; private set; }

        public Vector2 AnchoredPosition { get; private set; }
        public bool HasCustomPosition { get; private set; }

        public int EffectiveSizeMode => HasSizeOverride
            ? RuntimeSizeMode
            : InitialSizeMode;

        public void SetRuntimeSize(int sizeMode)
        {
            RuntimeSizeMode = NormalizeVisibleSizeMode(sizeMode);
            HasSizeOverride = true;
        }

        public void SetRuntimeColor(int colorMode)
        {
            RuntimeColorMode = Mathf.Clamp(colorMode, 0, 12);
            HasColorOverride = true;
        }

        public void ResetRuntimeStyle()
        {
            RuntimeSizeMode = InitialSizeMode;
            RuntimeColorMode = InitialColorMode;
            HasSizeOverride = false;
            HasColorOverride = false;
        }

        public void SetPosition(Vector2 anchoredPosition)
        {
            AnchoredPosition = anchoredPosition;
            HasCustomPosition = true;
        }

        private static int NormalizeVisibleSizeMode(int sizeMode)
        {
            // 0 是运行时菜单新增的小字号（0.8 倍）；1~4 保持原 1163 的可见字号语义。
            return Mathf.Clamp(sizeMode, 0, 4);
        }
    }
}
