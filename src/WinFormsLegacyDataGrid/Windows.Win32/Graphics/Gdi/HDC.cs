namespace Windows.Win32.Graphics.Gdi
{
    partial struct HDC
    {
        public bool IsNull => Value == 0;
    }
}
