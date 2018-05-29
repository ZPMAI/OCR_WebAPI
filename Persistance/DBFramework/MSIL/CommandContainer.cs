using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DBFramework.MSIL
{
    class CommandInfo
    {
        private string commandText;
        private CommandType commandType;
        private List<object> dataParameters;

        public string CommandText
        {
            get { return this.commandText; }
        }

        public CommandType CommandType
        {
            get { return this.commandType; }
        }


        /// ���캯��
        public CommandInfo(IDbCommand command)
        {
            this.commandText = command.CommandText;
            this.commandType = command.CommandType;
            this.dataParameters = new List<object>(command.Parameters.Count);

            foreach (ICloneable dataParameter in command.Parameters)
            {
                this.dataParameters.Add(dataParameter.Clone());
            }
        }


        /// <remarks>
        /// ORA-01861: �������ʽ�ַ�����ƥ��
        /// �� command ���л����ԭ������� IDbCommand.Parameters û�����
        /// 2011-03-28 14:46 ʹ�ö��ο�¡����
        /// </remarks>
        public List<IDataParameter> GetClonedParameters()
        {
            List<IDataParameter> clonedParameters = new List<IDataParameter>(this.dataParameters.Count);

            foreach (ICloneable dataParameter in this.dataParameters)
            {
                IDataParameter clonedParameter = dataParameter.Clone() as IDataParameter;

                clonedParameters.Add(clonedParameter);
            }

            return clonedParameters;
        }
    }


    public static class CommandContainer
    {
        private static Hashtable hashTable = Hashtable.Synchronized(new Hashtable());

        public static bool IsExist(string key)
        {
            return hashTable.ContainsKey(key);
        }


        public static void Add(string key, IDbCommand command)
        {
            if (IsExist(key)) { return; }
            
            CommandInfo commandInfo = new CommandInfo(command);
            hashTable.Add(key, commandInfo);
        }


        public static void Prepare(string key, IDbCommand command)
        {
            if (!IsExist(key)) { return; }

            CommandInfo commandInfo = hashTable[key] as CommandInfo;
            command.CommandText = commandInfo.CommandText;
            command.CommandType = commandInfo.CommandType;

            List<IDataParameter> dataParameters = commandInfo.GetClonedParameters();

            foreach (IDataParameter dataParameter in dataParameters)
            {
                command.Parameters.Add(dataParameter);
            }
        }
    }
}
