using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxTransactionManager3.DataLibrary
{
    public class ArrayType : DataType
    {
        /// <summary>
        /// List of TaxObjects stored in this ArrayType.
        /// </summary>
        [JsonProperty]
        private List<TaxObject> mArrayMembers;

        /// <summary>
        /// Number of TaxObjects in this ArrayType.
        /// </summary>
        [JsonIgnore]
        public int Count
        {
            get { return mArrayMembers.Count; }
        }

        public ArrayType(String name, TaxObject parent)
            : base(name, parent)
        {
            mArrayMembers = new List<TaxObject>();
        }

        /// <summary>
        /// Attempts to set the value of the DataType to the data Object.
        /// </summary>
        /// <param name="data"> Represents the new value for the DataType. </param>
        /// <returns> True if the data is successfully set. </returns>
        public override bool SetData(object data)
        {
            List<TaxObject> newData = data as List<TaxObject>;

            if (newData == null)
            {
                return false;
            }

            // If we get this far, then we are going to sub in the new array
            while (mArrayMembers.Count > 0)
            {
                RemoveArrayMember(mArrayMembers[0]);
            }

            // Now add back clones of each new TaxObject
            foreach (TaxObject o in newData)
            {
                AddArrayMember(o);
            }

            return true;
        }

        /// <summary>
        /// Returns the value of the DataType as an object.
        /// </summary>
        /// <returns> The value of the DataType as an object.</returns>
        public override object GetData()
        {
            return mArrayMembers;
        }

        /// <summary>
        /// Returns the value of the DataType as a List of TaxObjects. Throw an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as a List of TaxObjects, or an exception if the conversion can't be made. </returns>
        public override List<TaxObject> GetArrayType()
        {
            return mArrayMembers;
        }

        /// <summary>
        /// Clears all the items in the array.
        /// </summary>
        public virtual void ClearArray()
        {
            while (mArrayMembers.Count > 0)
            {
                RemoveArrayMember(mArrayMembers[0]);
            }
        }

        /// <summary>
        /// Returns the TaxObject at the passed in index in the array.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual TaxObject GetArrayMember(int index)
        {
            if (index > -1 && index < mArrayMembers.Count)
            {
                return mArrayMembers[index];
            }

            return null;
        }

        /// <summary>
        /// Adds TaxObject member to the end of the Array.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public virtual bool AddArrayMember(TaxObject member)
        {
            return InsertArrayMember(member);
        }

        /// <summary>
        /// Inserts TaxObject newMember after oldArrayMember. Throws an exception of oldArrayMember is not in this array.
        /// </summary>
        /// <param name="oldArrayMember"></param>
        /// <param name="newMember"></param>
        /// <returns></returns>
        public virtual bool InsertArrayMember(TaxObject oldArrayMember, TaxObject newMember)
        {
            if (!mArrayMembers.Contains(oldArrayMember))
            {
                throw new Exception("Tried to insert an array member after a member that didn't exist in the array.");
            }

            return InsertArrayMember(newMember, mArrayMembers.IndexOf(oldArrayMember));
        }

        /// <summary>
        /// Inserts TaxObject newMember at index in the array. Throws and exception if TaxObject is null or index is out of range.
        /// </summary>
        /// <param name="newMember"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual bool InsertArrayMember(TaxObject newMember, int index = -1)
        {
            if (index >= mArrayMembers.Count)
            {
                throw new Exception("Tried to insert an array member at an out-of-range index.");
            }

            if (newMember == null)
            {
                throw new Exception("Tried to insert a null TaxObject into an array.");
            }

            if (!mArrayMembers.Contains(newMember))
            {
                if (index < 0)
                {
                    mArrayMembers.Add(newMember);
                }
                else
                {
                    mArrayMembers.Insert(index, newMember);
                }

                newMember.AddBaseTypeReference(this);
                return true;
            }
           
            return false;
        }

        /// <summary>
        /// Removes TaxObject member from the array.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public virtual bool RemoveArrayMember(TaxObject member)
        {
            if (mArrayMembers.Contains(member))
            {
                mArrayMembers.Remove(member);
                member.RemoveBaseTypeReference(this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the array contains TaxObject o.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public virtual bool Contains(TaxObject o)
        {
            if (mArrayMembers.Contains(o))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the index of TaxObject o if it exists.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public virtual int IndexOf(TaxObject o)
        {
            return mArrayMembers.IndexOf(o);
        }

        /// <summary>
        /// Attempts to convert the string into the DataType data type. Returns true
        /// if the DataType is greater than the converted string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public override bool GreaterThan(string s)
        {
            try
            {
                int newInt = Convert.ToInt32(s);
                if (Count > newInt)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
