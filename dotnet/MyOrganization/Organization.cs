using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public int identifier { get; private set; }

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */


        public Position? Hire(Name person, string title)
        {
            return HireInPosition(root, person, title);
            //return null;
        }

        private Position? HireInPosition(Position position, Name person, string title)
        {
            if (position.GetTitle() == title && !position.IsFilled())
            {
                Employee newEmployee = new Employee(identifier, person);
                position.SetEmployee(newEmployee);
                return position;
            }

            foreach (var directReport in position.GetDirectReports())
            {
                var hiredPosition = HireInPosition(directReport, person, title);
                if (hiredPosition != null)
                {
                    return hiredPosition;
                }
            }

            return null;
        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
