using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxTransactionManager3.DataLibrary
{
    abstract public class DataType
    {
        /// <summary>
        /// This is a pointer to the TaxObject that contains this DataType.
        /// No need to serialize this as it will be reset each time a DataType is cloned.
        /// </summary>
        [JsonIgnore]
        private TaxObject mParent;
        [JsonIgnore]
        public TaxObject Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }

        /// <summary>
        /// Defines the name of the DataType which is used to identify it for document generation and
        /// rule analysis. Should not contain spaces.
        /// </summary>
        private string mName;
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        /// <summary>
        /// Defines the string that will be displayed in the property editor
        /// Unlike name, it can contain spaces.
        /// </summary>
        private string mDisplayName;
        public string DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
        }

        /// <summary>
        /// This defines the name of the DataType's group.
        /// </summary>
        private string mDisplayGroup;
        public string DisplayGroup
        {
            get { return mDisplayGroup; }
            set { mDisplayGroup = value; }
        }

        /// <summary>
        /// By default, data is assumed until it is explicitly set unassumed by the user
        /// </summary>
        private bool mAssumed;
        public bool Assumed
        {
            get { return mAssumed; }
            set { mAssumed = value; }
        }

        public DataType(String name, TaxObject parent)
        {
            Parent = parent;
            Name = name;
            DisplayName = string.Empty;
            DisplayGroup = string.Empty;
            Assumed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual DataType Clone(TaxObject parent)
        {
            DataType dt = DataCopier.CloneJson<DataType>(this);
            dt.Parent = parent;
            return dt;
        }

        /// <summary>
        /// Returns true if the DataType contains the same value as the bt.
        /// </summary>
        /// <param name="dt">The DataType to which we are comparing this DataType.</param>
        /// <returns></returns>
        public virtual bool Equals(DataType dt)
        {
            if (dt == null)
            {
                throw new Exception("Called Equals with null DataType");
            }

            if (dt.GetData() == GetData())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether this BaseType is Visible by checking the rules in the current Factory.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsVisible()
        {
            return DataInterface.sInterface.Factory.IsDataTypeVisible(this);
        }

        /// <summary>
        /// Evaluates all the rules in this BaseTypes EnabledRules and returns whether the DataType should
        /// be enabled.
        /// BaseTypes are enabled by default if there are no rules to check.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsEnabled()
        {
            return DataInterface.sInterface.Factory.IsDataTypeEnabled(this);
        }

        /// <summary>
        /// Evaluates all the rules in this BaseTypes EnabledRules and returns whether the DataType should
        /// be enabled.
        /// BaseTypes are enabled by default if there are no rules to check.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return DataInterface.sInterface.Factory.IsDataTypeValid(this);
        }

        /// <summary>
        /// Attempts to set the value of the DataType to the data Object.
        /// </summary>
        /// <param name="data"> Represents the new value for the DataType. </param>
        /// <returns> True if the data is successfully set. </returns>
        public abstract bool SetData(Object data);


        /// <summary>
        /// Returns the value of the DataType as an object.
        /// </summary>
        /// <returns> The value of the DataType as an object.</returns>
        public abstract object GetData();

        /// <summary>
        /// Returns the value of the DataType as a List of TaxObjects. Throw an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as a List of TaxObjects, or an exception if the conversion can't be made. </returns>
        public virtual List<TaxObject> GetArrayType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to ArrayType.");
        }

        /// <summary>
        /// Returns the value of the DataType as a boolean. Throw an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as a boolean, or an exception if the conversion can't be made. </returns>
        public virtual bool GetBoolType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to Bool.");
        }

        /// <summary>
        /// Returns the value of the DataType as a DateTime. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as a DateTime, or an exception if the conversion can't be made. </returns>
        public virtual DateTime GetDateType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to DateType.");
        }

        /// <summary>
        /// Returns the value of the DataType as a double. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as a double, or an exception if the conversion can't be made. </returns>
        public virtual double GetDoubleType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to Double.");
        }

        /// <summary>
        /// Returns the value of the DataType as an int. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as an int, or an exception if the conversion can't be made. </returns>
        public virtual int GetIntegerType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to Integer.");
        }

        /// <summary>
        /// Returns the value of the DataType as a pointer. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as an int, or an exception if the conversion can't be made. </returns>
        public virtual TaxObject GetPointerType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to PointerType.");
        }

        /// <summary>
        /// Returns the value of the DataType as a List of strings. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as an List of strings, or an exception if the conversion can't be made. </returns>
        public virtual List<String> GetStringArrayType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to StringArrayType.");
        }

        /// <summary>
        /// Returns the value of the DataType as a string. Throws an exception if the conversion can't be made.
        /// </summary>
        /// <returns> The value of the DataType as an string, or an exception if the conversion can't be made. </returns>
        public virtual String GetStringType()
        {
            throw new Exception("Couldn't convert DataType " + Name + " to String.");
        }


        /// <summary>
        /// Only applies to PointerTypes. Returns true if the TaxObject to which the PointerType points
        /// is equal to the string.
        /// </summary>
        /// <param name="s">Name of TaxObject type.</param>
        /// <returns>True if PointerType points to a TaxObject of type s.</returns>
        public virtual bool Is(string s) { return false; }

        /// <summary>
        /// Attempts to turn the string into the DataType data. Returns true
        /// if the data and string match.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual bool Equals(string s) { return false; }

        /// <summary>
        /// Attempts to turn the string into the DataType data type. Returns true
        /// if the data does not match the converted string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual bool NotEquals(string s) { return false; }

        /// <summary>
        /// Attempts to convert the string into the DataType data type. Returns true
        /// if the DataType is greater than the converted string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual bool GreaterThan(string s) { return false; }

        /// <summary>
        /// Attempts to convert the string into the DataType data type. Returns true
        /// if the DataType is less than the converted string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual bool LessThan(string s) { return false; }
    }
}
