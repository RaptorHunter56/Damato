using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damato_App.UserControls
{
    public partial class FileImage : UserControl
    {
        public FileImage(FileTypes image = FileTypes.@default)
        {
            Image = image;
            InitializeComponent();
        }
        public FileTypes Image { get; set; }


        //https://fileinfo.com/extension/wam
        public enum FileTypes
        {
            css, csv, dll, dmg, doc,
            eps, exe, flv, gif, gis,
            gpx, html, jp2, jpg, kml,
            kmz, mov, mp3, mpg, nmea,
            ogg, osm, otf, ppt, psd,
            rar, tar, tif, ttf, txt,
            wav, wma, woff, zip, @default
        }
    }
}
