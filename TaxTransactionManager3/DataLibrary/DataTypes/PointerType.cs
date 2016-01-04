using System;
using Newtonsoft.Json;

namespace TaxTransactionManager3.DataLibrary
{
    public class PointerType : DataType
    {
        /// <summary>
        /// Stores a pointer to a TaxObject.
        /// </summary>
        private TaxObject mTaxObject;
        [JsonIgnore]
        public TaxObject TaxObject
        {
            get { return mTaxObject; }
            set
            {
                mTaxObject = value;
                if (mTaxObject == null)
                {
                    mTaxObjectUniqueID = -1;
                }
                else
                {
                    mTaxObjectUniqueID = mTaxObject.UniqueID;
                }
            }
        }

        /// <summary>
        /// Stores the UniqueID of the current TaxObject, or -1 if TaxObject is null.
        /// When we serialize this DataType, we will only record this number
        /// </summary>
        [JsonProperty]
        private int mTaxObjectUniqueID;

        public PointerType(String name, TaxObject parent)
            : base(name, parent)
        {
            TaxObject = null;
        }

        /// <summary>
        /// Attempts to set the value of the DataType to the data Object.
        /// </summary>
        /// <param name="data"> Represents the new value for the DataType. </param>
        /// <returns> True if the data is successfully set. </returns>
        public override bool SetData(object data)
        {
            if (data == null)
            {
                if (TaxObject != null)
                {
                    TaxObject.RemoveBaseTypeReference(this);
                }
               
                TaxObject = null;
                return true;
            }
            else if (data is TaxObject)
            {
                TaxObject newObject = (TaxObject)data;
                if (newObject != TaxObject)
                {
                    if (TaxObject != null)
                    {
                        TaxObject.RemoveBaseTypeReference(this);
                    }

                    TaxObject = newObject;

                    if (TaxObject != null)
                    {
                        TaxObject.AddBaseTypeReference(this);
                    }


                    return true;
                }

                return false;
            }

            throw new Exception("Tried to set PointerType with bad data.");
        }

        /// <summary>
        /// Returns the value of the DataType as an object.
        /// </summary>
        /// <returns> The value of the DataType as an object.</returns>
        public override object GetData()
        {
            return TaxObject;
        }

        /// <summary>
        /// Returns the value of the DataType as a pointer. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as an int, or an exception if the conversion can't be made. </returns>
        public override TaxObject GetPointerType()
        {
            return TaxObject;
        }

        /// <summary>
        /// Returns a string representation of the data in the DataType.
        /// </summary>
        /// <returns>A string representing the data in the DataType</returns>
        public override string ToString()
        {
            if (TaxObject == null)
            {
                return "";
            }
            else
            {
                return TaxObject.GetDisplayString();
            }
        }

        /// <summary>
        /// Only applies to PointerTypes. Returns true if the TaxObject to which the PointerType points
        /// is equal to the string.
        /// </summary>
        /// <param name="s">Name of TaxObject type.</param>
        /// <returns>True if PointerType points to a TaxObject of type s.</returns>
        public override bool Is(string s)
        {
            if (TaxObject == null)
            {
                return false;
            }

            return TaxObject.GetObjectName() == s;
        }

        /// <summary>
        /// Attempts to turn the string into the DataType data type. Returns true
        /// if the data does not match the converted string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public override bool NotEquals(string s)
        {
            if (s == "null")
            {
                if (TaxObject == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
