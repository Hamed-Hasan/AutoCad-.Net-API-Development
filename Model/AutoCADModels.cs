using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCADApi.Models
{
    public class AutoCADFile
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public ICollection<Pin> Pins { get; set; } = new List<Pin>();
    }

    public class Pin
    {
        [Key]
        public int Id { get; set; }
        public int AutoCADFileId { get; set; }
        public AutoCADFile AutoCADFile { get; set; } = null!;
        public double X { get; set; }
        public double Y { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[] AudioClip { get; set; } = Array.Empty<byte>();
        public byte[] VideoClip { get; set; } = Array.Empty<byte>();
        public ModalContent ModalContent { get; set; } = null!;
    }

    public class ModalContent
    {
        [Key]
        public int Id { get; set; }
        public int PinId { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}