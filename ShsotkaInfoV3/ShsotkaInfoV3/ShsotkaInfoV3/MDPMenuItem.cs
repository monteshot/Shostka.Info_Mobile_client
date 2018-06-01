using ShsotkaInfoV3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShsotkaInfoV3
{

    public class MDPMenuItem
    {
        public MDPMenuItem()
        {
         //   TargetType = typeof(MDPDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        private Type _TargetType;
        public Type TargetType
        {
            get
            {
                if (Id == 0)
                {
                    return _TargetType = typeof(ItemsPage);
                }
                if (Id == 1)
                {
                    return _TargetType = typeof(AboutPage);
                }
                if (Id == 2)
                {

                    return _TargetType = typeof(SettingsPage);
                }
                if (Id == 3)
                {

                    return _TargetType = typeof(ProfilePage);
                }
                else
                {
                    return _TargetType = typeof(ItemsPage);
                }
            }
            set { _TargetType = value; }
        }
    }
}