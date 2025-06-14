﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (c) 2007-2025 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ShareX.HelpersLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public int X
        {
            get
            {
                return Left;
            }
            set
            {
                Right -= Left - value;
                Left = value;
            }
        }

        public int Y
        {
            get
            {
                return Top;
            }
            set
            {
                Bottom -= Top - value;
                Top = value;
            }
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = value + Left;
            }
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = value + Top;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(Left, Top);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
        {
        }

        public static implicit operator Rectangle(RECT r)
        {
            return new Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT rect)
            {
                return Equals(rect);
            }

            if (obj is Rectangle rectangle)
            {
                return Equals(new RECT(rectangle));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ((Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int Width;
        public int Height;

        public SIZE(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static explicit operator Size(SIZE s)
        {
            return new Size(s.Width, s.Height);
        }

        public static explicit operator SIZE(Size s)
        {
            return new SIZE(s.Width, s.Height);
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static explicit operator Point(POINT p)
        {
            return new Point(p.X, p.Y);
        }

        public static explicit operator POINT(Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public static WINDOWINFO Create()
        {
            WINDOWINFO wi = new WINDOWINFO();
            wi.cbSize = (uint)Marshal.SizeOf(typeof(WINDOWINFO));
            return wi;
        }
    }

    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public WindowShowStyle showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
    }

    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uCallbackMessage;
        public int uEdge;
        public RECT rc;
        public IntPtr lParam;

        public static APPBARDATA NewAPPBARDATA()
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(typeof(APPBARDATA));
            return abd;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_BLURBEHIND
    {
        public DWM_BB dwFlags;
        public bool fEnable;
        public IntPtr hRgnBlur;
        public bool fTransitionOnMaximized;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DWM_THUMBNAIL_PROPERTIES
    {
        public int dwFlags;
        public RECT rcDestination;
        public RECT rcSource;
        public byte opacity;
        public bool fVisible;
        public bool fSourceClientAreaOnly;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo
    {
        /// <summary>
        /// The size of the structure, in bytes. The caller must set this to sizeof(CURSORINFO).
        /// </summary>
        public int cbSize;

        /// <summary>
        /// The cursor state. This parameter can be one of the following values:
        /// 0 (The cursor is hidden.)
        /// CURSOR_SHOWING 0x00000001 (The cursor is showing.)
        /// CURSOR_SUPPRESSED 0x00000002 (Windows 8: The cursor is suppressed.This flag indicates that the system is not drawing the cursor because the user is providing input through touch or pen instead of the mouse.)
        /// </summary>
        public int flags;

        /// <summary>
        /// A handle to the cursor.
        /// </summary>
        public IntPtr hCursor;

        /// <summary>
        /// A structure that receives the screen coordinates of the cursor.
        /// </summary>
        public Point ptScreenPos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IconInfo
    {
        /// <summary>
        /// Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies an icon; FALSE specifies a cursor.
        /// </summary>
        public bool fIcon;

        /// <summary>
        /// The x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot spot is always in the center of the icon, and this member is ignored.
        /// </summary>
        public int xHotspot;

        /// <summary>
        /// The y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot spot is always in the center of the icon, and this member is ignored.
        /// </summary>
        public int yHotspot;

        /// <summary>
        /// The icon bitmask bitmap. If this structure defines a black and white icon, this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If this structure defines a color icon, this mask only defines the AND bitmask of the icon.
        /// </summary>
        public IntPtr hbmMask;

        /// <summary>
        /// A handle to the icon color bitmap. This member can be optional if this structure defines a black and white icon. The AND bitmask of hbmMask is applied with the SRCAND flag to the destination; subsequently, the color bitmap is applied (using XOR) to the destination by using the SRCINVERT flag.
        /// </summary>
        public IntPtr hbmColor;
    }

    /// <summary>
    /// Structure, which contains information for a single stream .
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct AVISTREAMINFO
    {
        /// <summary>
        /// Four-character code indicating the stream type.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int type;

        /// <summary>
        /// Four-character code of the compressor handler that will compress this video stream when it is saved.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int handler;

        /// <summary>
        /// Applicable flags for the stream.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int flags;

        /// <summary>
        /// Capability flags; currently unused.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int Capabilities;

        /// <summary>
        /// Priority of the stream.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I2)]
        public short priority;

        /// <summary>
        /// Language of the stream.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I2)]
        public short language;

        /// <summary>
        /// Time scale applicable for the stream.
        /// </summary>
        ///
        /// <remarks>Dividing <b>rate</b> by <b>scale</b> gives the playback rate in number of samples per second.</remarks>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int scale;

        /// <summary>
        /// Rate in an integer format.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int rate;

        /// <summary>
        /// Sample number of the first frame of the AVI file.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int start;

        /// <summary>
        /// Length of this stream.
        /// </summary>
        ///
        /// <remarks>The units are defined by <b>rate</b> and <b>scale</b>.</remarks>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int length;

        /// <summary>
        /// Audio skew. This member specifies how much to skew the audio data ahead of the video frames in interleaved files.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int initialFrames;

        /// <summary>
        /// Recommended buffer size, in bytes, for the stream.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int suggestedBufferSize;

        /// <summary>
        /// Quality indicator of the video data in the stream.
        /// </summary>
        ///
        /// <remarks>Quality is represented as a number between 0 and 10,000.</remarks>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int quality;

        /// <summary>
        /// Size, in bytes, of a single data sample.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int sampleSize;

        /// <summary>
        /// Dimensions of the video destination rectangle.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.Struct, SizeConst = 16)]
        public RECT rectFrame;

        /// <summary>
        /// Number of times the stream has been edited.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int editCount;

        /// <summary>
        /// Number of times the stream format has changed.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int formatChangeCount;

        /// <summary>
        /// Description of the stream.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
    }

    /// <summary>
    /// Structure, which contains information about a stream and how it is compressed and saved.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AVICOMPRESSOPTIONS
    {
        /// <summary>
        /// Four-character code indicating the stream type.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int type;

        /// <summary>
        /// Four-character code for the compressor handler that will compress this video stream when it is saved.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int handler;

        /// <summary>
        /// Maximum period between video key frames.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int keyFrameEvery;

        /// <summary>
        /// Quality value passed to a video compressor.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int quality;

        /// <summary>
        /// Video compressor data rate.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int bytesPerSecond;

        /// <summary>
        /// Flags used for compression.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int flags;

        /// <summary>
        /// Pointer to a structure defining the data format.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int format;

        /// <summary>
        /// Size, in bytes, of the data referenced by <b>format</b>.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int formatSize;

        /// <summary>
        /// Video-compressor-specific data; used internally.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int parameters;

        /// <summary>
        /// Size, in bytes, of the data referenced by <b>parameters</b>.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int parametersSize;

        /// <summary>
        /// Interleave factor for interspersing stream data with data from the first stream.
        /// </summary>
        ///
        [MarshalAs(UnmanagedType.I4)]
        public int interleaveEvery;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct BITMAPFILEHEADER
    {
        public static readonly short BM = 0x4d42;
        public short bfType;
        public int bfSize;
        public short bfReserved1;
        public short bfReserved2;
        public int bfOffBits;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct BITMAPINFOHEADER
    {
        [FieldOffset(0)]
        public uint biSize;
        [FieldOffset(4)]
        public int biWidth;
        [FieldOffset(8)]
        public int biHeight;
        [FieldOffset(12)]
        public ushort biPlanes;
        [FieldOffset(14)]
        public ushort biBitCount;
        [FieldOffset(16)]
        public BI_COMPRESSION biCompression;
        [FieldOffset(20)]
        public uint biSizeImage;
        [FieldOffset(24)]
        public int biXPelsPerMeter;
        [FieldOffset(28)]
        public int biYPelsPerMeter;
        [FieldOffset(32)]
        public uint biClrUsed;
        [FieldOffset(36)]
        public uint biClrImportant;
        [FieldOffset(40)]
        public uint bV5RedMask;
        [FieldOffset(44)]
        public uint bV5GreenMask;
        [FieldOffset(48)]
        public uint bV5BlueMask;
        [FieldOffset(52)]
        public uint bV5AlphaMask;
        [FieldOffset(56)]
        public uint bV5CSType;
        [FieldOffset(60)]
        public CIEXYZTRIPLE bV5Endpoints;
        [FieldOffset(96)]
        public uint bV5GammaRed;
        [FieldOffset(100)]
        public uint bV5GammaGreen;
        [FieldOffset(104)]
        public uint bV5GammaBlue;
        [FieldOffset(108)]
        public uint bV5Intent;
        [FieldOffset(112)]
        public uint bV5ProfileData;
        [FieldOffset(116)]
        public uint bV5ProfileSize;
        [FieldOffset(120)]
        public uint bV5Reserved;

        public const int DIB_RGB_COLORS = 0;

        public BITMAPINFOHEADER(int width, int height, ushort bpp)
        {
            biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            biPlanes = 1;
            biCompression = BI_COMPRESSION.BI_RGB;
            biWidth = width;
            biHeight = height;
            biBitCount = bpp;
            biSizeImage = (uint)(width * height * (bpp >> 3));
            biXPelsPerMeter = 0;
            biYPelsPerMeter = 0;
            biClrUsed = 0;
            biClrImportant = 0;
            bV5RedMask = (uint)255 << 16;
            bV5GreenMask = (uint)255 << 8;
            bV5BlueMask = (uint)255;
            bV5AlphaMask = (uint)255 << 24;
            bV5CSType = 1934772034;
            bV5Endpoints = new CIEXYZTRIPLE();
            bV5Endpoints.ciexyzBlue = new CIEXYZ(0);
            bV5Endpoints.ciexyzGreen = new CIEXYZ(0);
            bV5Endpoints.ciexyzRed = new CIEXYZ(0);
            bV5GammaRed = 0;
            bV5GammaGreen = 0;
            bV5GammaBlue = 0;
            bV5Intent = 4;
            bV5ProfileData = 0;
            bV5ProfileSize = 0;
            bV5Reserved = 0;
        }

        public uint OffsetToPixels
        {
            get
            {
                if (biCompression == BI_COMPRESSION.BI_BITFIELDS)
                {
                    return biSize + (3 * 4);
                }

                return biSize;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIEXYZ
    {
        public uint ciexyzX;
        public uint ciexyzY;
        public uint ciexyzZ;

        public CIEXYZ(uint FXPT2DOT30)
        {
            ciexyzX = FXPT2DOT30;
            ciexyzY = FXPT2DOT30;
            ciexyzZ = FXPT2DOT30;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIEXYZTRIPLE
    {
        public CIEXYZ ciexyzRed;
        public CIEXYZ ciexyzGreen;
        public CIEXYZ ciexyzBlue;
    }

    public struct INPUT
    {
        public InputType Type;
        public InputUnion Data;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT Mouse;

        [FieldOffset(0)]
        public KEYBDINPUT Keyboard;

        [FieldOffset(0)]
        public HARDWAREINPUT Hardware;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public VirtualKeyCode wVk;
        public ushort wScan;
        public KeyboardEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    [ComImport]
    [Guid("0000010D-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IViewObject
    {
        void Draw([MarshalAs(UnmanagedType.U4)] uint dwAspect, int lindex, IntPtr pvAspect, [In] IntPtr ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [MarshalAs(UnmanagedType.Struct)] ref RECT lprcBounds, [In] IntPtr lprcWBounds, IntPtr pfnContinue, [MarshalAs(UnmanagedType.U4)] uint dwContinue);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public uint cbSize;
        public IntPtr hwnd;
        public uint dwFlags;
        public uint uCount;
        public uint dwTimeout;
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct SCROLLINFO
    {
        public uint cbSize;
        public uint fMask;
        public int nMin;
        public int nMax;
        public uint nPage;
        public int nPos;
        public int nTrackPos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        public int nLength;
        public IntPtr lpSecurityDescriptor;
        public int bInheritHandle;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct STARTUPINFO
    {
        public int cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public int dwX;
        public int dwY;
        public int dwXSize;
        public int dwYSize;
        public int dwXCountChars;
        public int dwYCountChars;
        public int dwFillAttribute;
        public int dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public const int NAMESIZE = 80;
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGELISTDRAWPARAMS
    {
        public int cbSize;
        public IntPtr himl;
        public int i;
        public IntPtr hdcDst;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int xBitmap; // x offest from the upperleft of bitmap
        public int yBitmap; // y offset from the upperleft of bitmap
        public int rgbBk;
        public int rgbFg;
        public int fStyle;
        public int dwRop;
        public int fState;
        public int Frame;
        public int crEffect;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGEINFO
    {
        public IntPtr hbmImage;
        public IntPtr hbmMask;
        public int Unused1;
        public int Unused2;
        public RECT rcImage;
    }

    [ComImportAttribute()]
    [GuidAttribute("46EB5926-582E-4017-9FDF-E8998DAA0950")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IImageList
    {
        [PreserveSig]
        int Add(IntPtr hbmImage, IntPtr hbmMask, ref int pi);

        [PreserveSig]
        int ReplaceIcon(int i, IntPtr hicon, ref int pi);

        [PreserveSig]
        int SetOverlayImage(int iImage, int iOverlay);

        [PreserveSig]
        int Replace(int i, IntPtr hbmImage, IntPtr hbmMask);

        [PreserveSig]
        int AddMasked(IntPtr hbmImage, int crMask, ref int pi);

        [PreserveSig]
        int Draw(ref IMAGELISTDRAWPARAMS pimldp);

        [PreserveSig]
        int Remove(int i);

        [PreserveSig]
        int GetIcon(int i, int flags, ref IntPtr picon);

        [PreserveSig]
        int GetImageInfo(int i, ref IMAGEINFO pImageInfo);

        [PreserveSig]
        int Copy(int iDst, IImageList punkSrc, int iSrc, int uFlags);

        [PreserveSig]
        int Merge(int i1, IImageList punk2, int i2, int dx, int dy, ref Guid riid, ref IntPtr ppv);

        [PreserveSig]
        int Clone(ref Guid riid, ref IntPtr ppv);

        [PreserveSig]
        int GetImageRect(int i, ref RECT prc);

        [PreserveSig]
        int GetIconSize(ref int cx, ref int cy);

        [PreserveSig]
        int SetIconSize(int cx, int cy);

        [PreserveSig]
        int GetImageCount(ref int pi);

        [PreserveSig]
        int SetImageCount(int uNewCount);

        [PreserveSig]
        int SetBkColor(int clrBk, ref int pclr);

        [PreserveSig]
        int GetBkColor(ref int pclr);

        [PreserveSig]
        int BeginDrag(int iTrack, int dxHotspot, int dyHotspot);

        [PreserveSig]
        int EndDrag();

        [PreserveSig]
        int DragEnter(IntPtr hwndLock, int x, int y);

        [PreserveSig]
        int DragLeave(IntPtr hwndLock);

        [PreserveSig]
        int DragMove(int x, int y);

        [PreserveSig]
        int SetDragCursorImage(ref IImageList punk, int iDrag, int dxHotspot, int dyHotspot);

        [PreserveSig]
        int DragShowNolock(int fShow);

        [PreserveSig]
        int GetDragImage(ref POINT ppt, ref POINT pptHotspot, ref Guid riid, ref IntPtr ppv);

        [PreserveSig]
        int GetItemFlags(int i, ref int dwFlags);

        [PreserveSig]
        int GetOverlayImage(int iOverlay, ref int piIndex);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPV5HEADER
    {
        public uint bV5Size;
        public int bV5Width;
        public int bV5Height;
        public ushort bV5Planes;
        public ushort bV5BitCount;
        public uint bV5Compression;
        public uint bV5SizeImage;
        public int bV5XPelsPerMeter;
        public int bV5YPelsPerMeter;
        public ushort bV5ClrUsed;
        public ushort bV5ClrImportant;
        public ushort bV5RedMask;
        public ushort bV5GreenMask;
        public ushort bV5BlueMask;
        public ushort bV5AlphaMask;
        public ushort bV5CSType;
        public IntPtr bV5Endpoints;
        public ushort bV5GammaRed;
        public ushort bV5GammaGreen;
        public ushort bV5GammaBlue;
        public ushort bV5Intent;
        public ushort bV5ProfileData;
        public ushort bV5ProfileSize;
        public ushort bV5Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TimeCaps
    {
        public uint wPeriodMin;
        public uint wPeriodMax;
    }

    [ComImportAttribute()]
    [GuidAttribute("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItemImageFactory
    {
        void GetImage([In, MarshalAs(UnmanagedType.Struct)] SIZE size, [In] SIIGBF flags, [Out] out IntPtr phbm);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifRWData
    {
        public IntPtr data; // uint8_t*
        public IntPtr size; // size_t - Use IntPtr for cross-platform size_t
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifRGBImage
    {
        public uint width;
        public uint height;
        public uint depth;
        public AvifRGBFormat format;

        public AvifChromaUpsampling chromaUpsampling;
        public AvifChromaDownsampling chromaDownsampling;
        public uint avoidLibYUV; // Actually avifBool (int) in C, represented as uint here for simplicity or potential mapping
        public uint ignoreAlpha; // Actually avifBool (int)
        public uint alphaPremultiplied; // Actually avifBool (int)
        public uint isFloat; // Actually avifBool (int)
        public int maxThreads;

        public IntPtr pixels;
        public uint rowBytes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifPixelAspectRatioBox
    {
        public uint hSpacing;
        public uint vSpacing;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifCleanApertureBox
    {
        public uint widthN;
        public uint widthD;
        public uint heightN;
        public uint heightD;
        public uint horizOffN;
        public uint horizOffD;
        public uint vertOffN;
        public uint vertOffD;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifImageRotation
    {
        public byte angle; // legal values: [0-3]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifImageMirror
    {
        public byte axis; // legal values: [0, 1]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifContentLightLevelInformationBox
    {
        public ushort maxCLL; // uint16_t
        public ushort maxPALL; // uint16_t
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct AvifImage
    {
        // --- Image information ---

        /// <summary>
        /// Width of the image in pixels.
        /// </summary>
        public uint Width;

        /// <summary>
        /// Height of the image in pixels.
        /// </summary>
        public uint Height;

        /// <summary>
        /// Bit depth of the image planes (8, 10, or 12). All planes must share this depth.
        /// If depth > 8, all planes are uint16_t internally in libavif.
        /// </summary>
        public uint Depth;

        /// <summary>
        /// The pixel format (chroma subsampling).
        /// </summary>
        public AvifPixelFormat YuvFormat;

        /// <summary>
        /// The YUV color range (limited or full).
        /// </summary>
        public AvifRange YuvRange;

        /// <summary>
        /// The positioning of chroma samples relative to luma samples.
        /// </summary>
        public AvifChromaSamplePosition YuvChromaSamplePosition;

        /// <summary>
        /// Pointers to the start of each YUV plane's pixel data (Y, U, V).
        /// Size is AVIF_PLANE_COUNT_YUV (usually 3).
        /// Accessing the data requires marshalling from these IntPtrs.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AvifConstants.AVIF_PLANE_COUNT_YUV)]
        public IntPtr[] YuvPlanes; // uint8_t * yuvPlanes[AVIF_PLANE_COUNT_YUV];

        /// <summary>
        /// The number of bytes per row for each YUV plane (stride).
        /// Size is AVIF_PLANE_COUNT_YUV (usually 3).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AvifConstants.AVIF_PLANE_COUNT_YUV)]
        public uint[] YuvRowBytes; // uint32_t yuvRowBytes[AVIF_PLANE_COUNT_YUV];

        /// <summary>
        /// If true, libavif allocated the YUV plane buffers and will free them on destroy.
        /// If false, the caller owns the YUV plane buffers.
        /// Mapped from avifBool (int). Use I4 for interop.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public bool ImageOwnsYUVPlanes; // avifBool is 'int' in avif.h

        /// <summary>
        /// Pointer to the start of the alpha plane's pixel data. NULL if no alpha.
        /// Accessing the data requires marshalling from this IntPtr.
        /// </summary>
        public IntPtr AlphaPlane; // uint8_t * alphaPlane;

        /// <summary>
        /// The number of bytes per row for the alpha plane (stride). 0 if no alpha.
        /// </summary>
        public uint AlphaRowBytes;

        /// <summary>
        /// If true, libavif allocated the alpha plane buffer and will free it on destroy.
        /// If false, the caller owns the alpha plane buffer.
        /// Mapped from avifBool (int). Use I4 for interop.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public bool ImageOwnsAlphaPlane; // avifBool is 'int' in avif.h

        /// <summary>
        /// Indicates whether the alpha channel is premultiplied.
        /// Mapped from avifBool (int). Use I4 for interop.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public bool AlphaPremultiplied; // avifBool is 'int' in avif.h

        // --- Color Profiles ---

        /// <summary>
        /// ICC profile data. Check icc.size > 0 for presence.
        /// </summary>
        public AvifRWData Icc;

        // --- CICP (Coding-Independent Code Points) Information ---
        // Used if ICC profile is not present. Stored in AV1 payload and AVIF 'colr' box.

        /// <summary>
        /// Color primaries (e.g., BT.709, BT.2020). Part of CICP.
        /// </summary>
        public AvifColorPrimaries ColorPrimaries;

        /// <summary>
        /// Transfer characteristics (e.g., sRGB, PQ, HLG). Part of CICP.
        /// </summary>
        public AvifTransferCharacteristics TransferCharacteristics;

        /// <summary>
        /// Matrix coefficients (e.g., BT.709, BT.601, BT.2020). Part of CICP.
        /// </summary>
        public AvifMatrixCoefficients MatrixCoefficients;

        // --- CLLI (Content Light Level Information) ---

        /// <summary>
        /// Content Light Level Information (max/average light levels).
        /// Stored in AVIF 'clli' box. (0, 0) means unknown/unavailable.
        /// </summary>
        public AvifContentLightLevelInformationBox Clli;

        // --- Transformations ---
        // These metadata values are encoded/decoded based on transformFlags.
        // They DO NOT modify the pixel data buffers directly during decode.

        /// <summary>
        /// Flags indicating which transformation boxes (pasp, clap, irot, imir) are present and active.
        /// </summary>
        public AvifTransformFlags TransformFlags;

        /// <summary>
        /// Pixel Aspect Ratio ('pasp' box). Active if (transformFlags & AVIF_TRANSFORM_PASP) != 0.
        /// </summary>
        public AvifPixelAspectRatioBox Pasp;

        /// <summary>
        /// Clean Aperture ('clap' box). Active if (transformFlags & AVIF_TRANSFORM_CLAP) != 0.
        /// Defines the region of the image intended for display.
        /// </summary>
        public AvifCleanApertureBox Clap;

        /// <summary>
        /// Image Rotation ('irot' box). Active if (transformFlags & AVIF_TRANSFORM_IROT) != 0.
        /// Specifies rotation in 90-degree increments (0-3).
        /// </summary>
        public AvifImageRotation Irot;

        /// <summary>
        /// Image Mirroring ('imir' box). Active if (transformFlags & AVIF_TRANSFORM_IMIR) != 0.
        /// Specifies horizontal (1) or vertical (0) mirroring.
        /// </summary>
        public AvifImageMirror Imir;

        // --- Metadata ---

        /// <summary>
        /// EXIF metadata payload. Check exif.size > 0 for presence.
        /// </summary>
        public AvifRWData Exif;

        /// <summary>
        /// XMP metadata payload. Check xmp.size > 0 for presence.
        /// </summary>
        public AvifRWData Xmp;

        // --- Other Properties ---

        /// <summary>
        /// Pointer to an array of other image item properties (like 'auxC', 'prem'). NULL if numProperties is 0.
        /// Interaction requires defining AvifImageItemProperty struct and marshalling.
        /// </summary>
        public IntPtr Properties; // avifImageItemProperty *

        /// <summary>
        /// Number of elements in the 'properties' array. Use IntPtr for size_t cross-platform compatibility.
        /// Read as: (int)NumProperties on 32-bit, (long)NumProperties on 64-bit, or use nuint/nint in modern C#.
        /// </summary>
        public IntPtr NumProperties; // size_t

        // --- Gain Map ---

        /// <summary>
        /// Gain map image and metadata. NULL if no gain map is present.
        /// Owned by the avifImage. Interaction requires defining AvifGainMap struct and marshalling.
        /// gainMap->image->transformFlags is always AVIF_TRANSFORM_NONE.
        /// </summary>
        public IntPtr GainMap; // avifGainMap *

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifDecoder
    {
        // Settings - Must match the order and size in avif.h up to 'image'
        public AvifCodecChoice codecChoice;
        public int maxThreads;
        public AvifDecoderSource requestedSource;
        public int allowProgressive; // avifBool
        public int allowIncremental; // avifBool
        public int ignoreExif; // avifBool
        public int ignoreXMP; // avifBool
        public uint imageSizeLimit;
        public uint imageDimensionLimit;
        public uint imageCountLimit;
        public uint strictFlags; // avifStrictFlags

        // --- Outputs start here ---
        public IntPtr image; // avifImage* - This is what we need
        // Add other public output fields *after* image if needed by later code,
        // but for GetImage, we only need the offset up to 'image'.
        // int imageIndex;
        // int imageCount;
        // ... and so on
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct avifEncoder // Partial representation for setting options
    {
        // --- Fields must match avifEncoder in avif.h up to the ones we set ---
        public AvifCodecChoice codecChoice;

        // Defaults to 1. If < 2, multithreading is disabled. See also 'Understanding maxThreads' above. https://github.com/AOMediaCodec/libavif/blob/bb066689f9a5aba2b72732864ccb4a89e0ef520e/include/avif/avif.h#L903
        public int maxThreads;

        // Speed range: [AVIF_SPEED_SLOWEST - AVIF_SPEED_FASTEST]. Slower should make for a better quality
        // image in fewer bytes. AVIF_SPEED_DEFAULT means "Leave the AV1 codec to its default speed settings".
        // If avifEncoder uses rav1e, the speed value is directly passed through (0-10). If libaom is used,
        // a combination of settings are tweaked to simulate this speed range.
        public int speed;

        public int keyframeInterval;
        public ulong timescale;
        public int repetitionCount;
        public uint extraLayerCount;

        // Quality settings
        public int quality;             // Target field (0-100, 100=best)
        public int qualityAlpha;        // Target field (0-100, 100=best)

        // Tiling fields
        public int tileRowsLog2;
        public int tileColsLog2;

        // If autoTiling is set to AVIF_TRUE, libavif ignores tileRowsLog2 and tileColsLog2 and
        // automatically chooses suitable tiling values.
        public int autoTiling;

        // Scaling mode
        public AvifScalingMode scalingMode;

        // --- We don't need fields after this point for setting quality/speed ---
        // IntPtr data; // Internal
        // IntPtr csOptions; // Internal
        // uint headerFormat; // Added v1.1.0
        // int qualityGainMap; // Added v1.2.0
        // ... other potential future fields or experimental ones ...
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifFraction
    {
        public int n;
        public int d;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AvifScalingMode
    {
        public AvifFraction horizontal;
        public AvifFraction vertical;
    }
}