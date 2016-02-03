using System;
using PainLogUWP.Enums;
using PainLogUWP.Interfaces;

namespace PainLogUWP.Models
{
    public class Pain : IElement
    {
        public Pain()
        {
            Id = Guid.NewGuid();
        }

        public string BodyPart { get; set; }
        public BodySide BodySide { get; set; }
        public Guid Id { get; set; }
        public PainType PainType { get; set; }
    }
}