﻿using artaimusDBlib;
using System.ComponentModel.DataAnnotations;

namespace ArcheryProject.Models
{
    public class EventModel
    {
        //Pacour Event Setup
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        public List<ParcourModel> AvailableParcours { get; set; }

        public string SelectedParcours { get; set; }

        public string? Name { get; set; }

        public static Dictionary<CountType, string> countTypeNames =  new Dictionary<CountType, string>
        {
            { CountType.PfeilWertung3, "PfeilWertung3" },
            { CountType.PfeilWertung2, "PfeilWertung2" }
        };
        public enum CountType { PfeilWertung3, PfeilWertung2 }
        public CountType CountTypeValue { get; set; } = CountType.PfeilWertung3;



        //public static List<Parcour> tmpParcourList = new List<Parcour> { };




        //Match
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////
        /// </summary>
        
        [Display(Name = "Username/Email")]
        [Required(ErrorMessage = "Required")]
        public string? ModalLoginName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required")]
        public string? ModalLoginPassword { get; set; }

        public int? Id { get; set; } //0

        public Parcour? Parcours { get; set; }  //id & anzahl ziele = spalten

        public List<string> PlayerList = new List<string> { };

        public List<int> Points = new List<int> { };


    }
}
