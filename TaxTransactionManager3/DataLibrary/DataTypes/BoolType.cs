using System;

namespace TaxTransactionManager3.DataLibrary
{
    class BoolType : DataType
    {
        public static String TRUE_STRING = "True";
        public static String FALSE_STRING = "False";

        private bool mBool;
        public bool Bool
        {
            get { return mBool; }
            set { mBool = value; }
        }

        public BoolType(String name, String defaultValue, TaxObject parent) : base(name, parent)
        {
            Bool = false;

            if (defaultValue != null)
            {
                if (defaultValue == TRUE_STRING)
                {
                    Bool = true;
                }
            }
        }

        /// <summary>
        /// Attempts to set the value of the DataType to the data Object.
        /// </summary>
        /// <param name="data"> Represents the new value for the DataType. </param>
        /// <returns> True if the data is successfully set. </returns>
        public override bool SetData(Object data)
        {
            if (data == null)
            {
                throw new Exception("Tried to set null in Boolean " + this.Name);
            }

            if (data is bool)
            {
                bool newBool = (bool)data;
                if (Bool != newBool)
                {
                    Bool = newBool;
                    return true;
                }
                return false;
            }
            else if (data is String)
            {
                String stringData = (String)data;
                if (stringData == TRUE_STRING)
                {
                    if (Bool != true)
                    {
                        Bool = true;
                        return true;
                    }
                    return false;
                }
                else if (stringData == FALSE_STRING)
                {
                    if (Bool != false)
                    {
                        Bool = false;
                        return true;
                    }

                    return false;
                }
                else
                {
                    throw new Exception("Tried to set bad String in Boolean " + this.Name);
                }
            }

            throw new Exception("Tried to set bad data in Boolean " + this.Name);
        }

        /// <summary>
        /// Returns the value of the DataType as an object.
        /// </summary>
        /// <returns> The value of the DataType as an object.</returns>
        public override object GetData()
        {
            return Bool;
        }

        /// <summary>
        /// Returns the value of the DataType as a boolean. Throw an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as a boolean, or an exception if the conversion can't be made. </returns>
        public override bool GetBoolType()
        {
            return Bool;
        }
        
        /// <summary>
        /// Returns a string representation of the data in the DataType.
        /// </summary>
        /// <returns>A string representing the data in the DataType</returns>
        public override string ToString()
        {
            if (mBool)
            {
                return TRUE_STRING;
            }
           
            return FALSE_STRING;
        }

        /// <summary>
        /// Attempts to turn the string into the DataType data. Returns true
        /// if the data and string match.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public override bool Equals(string s)
        {
            if (s == TRUE_STRING)
            {
                return Bool;
            }
            else
            {
                return !Bool;
            }
        }

        /// <summary>
        /// Attempts to turn the string into the DataType data type. Returns true
        /// if the data does not match the converted string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public override bool NotEquals(string s)
        {
            if (s == TRUE_STRING)
            {
                return !Bool;
            }
            else
            {
                return Bool;
            }
        }
    }
}
