using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using OCR.Model;
using OCR.DAL;

namespace OCR.BLL
{
    /// <summary>
    /// ctos api
    /// </summary>
    public class CtosAPIBLL
    {
        public static string SUCCESSCODE = "0";

        /// <summary>
        /// 该功能用于验证用户信息 ，并返回相应的TICKET号
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <param name="PASSWORD"></param>
        /// <param name="CLIENTIP"></param>
        /// <returns></returns>
        public static CtosResult SM001001(string USER_ID, string PASSWORD, string CLIENTIP)
        {
            WebReferenceTest.Service api = new OCR.BLL.WebReferenceTest.Service();
            string parms = string.Format("USER_ID:'{0}',PASSWORD:'{1}',CLIENTIP:'{2}'", USER_ID, PASSWORD, CLIENTIP);
            
            string rs = api.SM001001(parms);

            //<CTOSRESULT><RETURNINFO><ERRORCODE>0</ERRORCODE><ERRORMSG>success</ERRORMSG></RETURNINFO><SM001001><TICKET_ID>OUphAgIXmOAKokUqLnP2P2mw3Hxvq73EDswQjDKeKubxwYkMMzBCsrTt/IpxhKmc9jyAf3wJimVW3wG5+s6mEBOBtHq+FHNMFfy6ANy+3b9IdUHDjGKJ8/Bq7aFaSBu5tToSM+wm8pkm0Bo7W4GE8AcIIkT+xxeuMSgmAaCLFpc5JhRKuFN0LxCgs69ALQHZjVuEf3il6bSKtsRySREK3CZndTPAHtQX1jni2M2DtlFopGOkP7CRFQ==</TICKET_ID></SM001001></CTOSRESULT>

            return XMLParse(rs, new string[] { "SM001001" }, false);
        }

        /// <summary>
        /// 用户注销 
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult SM001002(string USER_ID, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("USER_ID:'{0}',TICKET_ID:'{1}'", USER_ID, TICKET_ID);

            string rs = api.SM001002(parms);

            return XMLParse(rs, new string[] { "SM001002" }, false);
        }

        /// <summary>
        /// 无线终端登录(指挥手登录)
        /// </summary>
        /// <param name="USERNAME"></param>
        /// <param name="PASSWORD"></param>
        /// <param name="TERMINALNO"></param>
        /// <param name="CLIENTIP"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007001(string USERNAME, string PASSWORD, string TERMINALNO, string CLIENTIP, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("USERNAME:'{0}',PASSWORD:'{1}',TERMINALNO:'{2}',CLIENTIP:'{3}',TICKET_ID:'{4}'",
                USERNAME, PASSWORD, TERMINALNO, CLIENTIP, TICKET_ID);

            string rs = api.OP007001(parms);

            return XMLParse(rs, new string[] { "OP007001" }, false);
        }

        /// <summary>
        /// 无线终端注销(指挥手退出)
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <param name="TERMINALNO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007002(string USER_ID, string TERMINALNO, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("USER_ID:'{0}',TERMINALNO:'{1}',TICKET_ID:'{2}'", USER_ID, TERMINALNO, TICKET_ID);

            string rs = api.OP007002(parms);

            return XMLParse(rs, new string[] { "OP007002" }, false);
        }

        /// <summary>
        /// 用户作业退出日志记录
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <param name="DEVICENO"></param>
        /// <param name="QCNO"></param>
        /// <param name="QCDRIVER"></param>
        /// <param name="CONTRACTORCODE"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007006(string USER_ID, string DEVICENO, string QCNO, string QCDRIVER, string CONTRACTORCODE, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("USER_ID:'{0}',DEVICENO:'{1}',QCNO:'{2}',QCDRIVER:'{3}',CONTRACTORCODE:'{4}',TICKET_ID:'{5}'", USER_ID, DEVICENO, QCNO,
                QCDRIVER, CONTRACTORCODE, TICKET_ID);

            string rs = api.OP007006(parms);

            return XMLParse(rs, new string[] { "OP007006" }, false);
        }

        /// <summary>
        /// 船舶初始化--检查装卸船信息
        /// </summary>
        /// <param name="QCNO"></param>
        /// <param name="QCDRIVER"></param>
        /// <param name="CONTRACTORCODE"></param>
        /// <param name="USERID"></param>
        /// <param name="VESSELNAME"></param>
        /// <param name="VOY"></param>
        /// <param name="DEVICENO"></param>
        /// <param name="OPTYPE"></param>
        /// <param name="BERTHNO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007030(string QCNO, string QCDRIVER, string CONTRACTORCODE, string USERID,
            string VESSELNAME, string VOY, string DEVICENO, string OPTYPE, string BERTHNO, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("QCNO:'{0}',QCDRIVER:'{1}',CONTRACTORCODE:'{2}',USERID:'{3}',VESSELNAME:'{4}',VOY:'{5}',DEVICENO:'{6}',OPTYPE:'{7}',BERTHNO:'{8}',TICKET_ID:'{9}'",
                QCNO, QCDRIVER, CONTRACTORCODE, USERID, VESSELNAME, VOY, DEVICENO, OPTYPE, BERTHNO, TICKET_ID);

            string rs = api.OP007030(parms);

            return XMLParse(rs, new string[] { "OP007030" }, false);
        }

        /// <summary>
        /// 根据箱号索卸船箱指令信息
        /// </summary>
        /// <param name="CNTRSUFFIXLIST"></param>
        /// <param name="VELALIASE"></param>
        /// <param name="WORKPOINTNO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007031(string CNTRSUFFIX, string VELALIASE, string TICKET_ID, string POW)
        {
            //CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            //string parms = string.Format("CNTRSUFFIX:'{0}',VELALIASE:'{1}',TICKET_ID:'{2}'",
            //    CNTRSUFFIX, VELALIASE, TICKET_ID);

            //string rs = api.OP007032(parms);
            //string rs = api.OP007031(parms);

            CtosWebRef82.WSVCDataAccess api = new OCR.BLL.CtosWebRef82.WSVCDataAccess();
            string parms = string.Format("CNTRSUFFIX:'{0}',VELALIASE:'{1}',POW:'{2}'",
                CNTRSUFFIX, VELALIASE, POW);

            string parmsBase64 = EncodeBase64(Encoding.UTF8, parms);

            byte[] bytes = api.ExecBiz("OP007031", parmsBase64, TICKET_ID);
            string rs = Encoding.UTF8.GetString(bytes);

            //OP0070310WI_WORKITEMSCONTAINERIDWORKITEMNOINAIMCONTAINERTYPECONTAINERHEIGHTISDISCHECKCTNISDISCHECKSEALSPECIALREQUIREMENTSCONTAINERNOSHORTCODEWORKPOINTNOSETUPTEMPTEMPTYPEVESSELCOMPANYSEALNOVESSELCOMPANYSEALNOLISTCIQSEALNOCIQSEALNOLISTISAUTOLISTSEALNOOVERFRONTOVERBEHINDOVERLEFTOVERRIGHTOVERTOPEMPTYFULLISOCODEISBINDBINDSEQUENCEDAMAGECODEGRADEIDCONTAINERSIZESOURCEPOSTARGETSTATUSISOVERTOPISREEFCARRYDEVICEPOSONTRUCKISIMDGISOVERFLOWCONTAINERSHORTISOISDISCHECKSEALLISAUTOLISTSEALLNOISDISCHECKSEALCISAUTOLISTSEALCNO58238590979365823859098703IDV9.6NNMRKU23312544DV9C18NF45G1N400300012ECNNN0391MNN

            //return XMLParse(rs, new string[] { "WI_WORKITEMS", "DANGERINFO" }, true);
            return ParseCtos(rs);
        }

        /// <summary>
        /// 根据箱号索卸船箱指令信息
        /// </summary>
        /// <param name="CNTRSUFFIXLIST"></param>
        /// <param name="VELALIASE"></param>
        /// <param name="WORKPOINTNO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007031B(string CNTRSUFFIX, string VELALIASE, string TICKET_ID, string POW)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("CNTRSUFFIX:'{0}',VELALIASE:'{1}',TICKET_ID:'{2}'",
                CNTRSUFFIX, VELALIASE, TICKET_ID);

            //string rs = api.OP007032(parms);
            string rs = api.OP007031(parms);

            //CtosWebRef82.WSVCDataAccess api = new OCR.BLL.CtosWebRef82.WSVCDataAccess();
            //string parms = string.Format("CNTRSUFFIX:'{0}',VELALIASE:'{1}',POW:'{2}'",
            //    CNTRSUFFIX, VELALIASE, POW);

            //string parmsBase64 = EncodeBase64(Encoding.UTF8, parms);

            //byte[] bytes = api.ExecBiz("OP007031", parmsBase64, TICKET_ID);
            //string rs = Encoding.UTF8.GetString(bytes);

            //OP0070310WI_WORKITEMSCONTAINERIDWORKITEMNOINAIMCONTAINERTYPECONTAINERHEIGHTISDISCHECKCTNISDISCHECKSEALSPECIALREQUIREMENTSCONTAINERNOSHORTCODEWORKPOINTNOSETUPTEMPTEMPTYPEVESSELCOMPANYSEALNOVESSELCOMPANYSEALNOLISTCIQSEALNOCIQSEALNOLISTISAUTOLISTSEALNOOVERFRONTOVERBEHINDOVERLEFTOVERRIGHTOVERTOPEMPTYFULLISOCODEISBINDBINDSEQUENCEDAMAGECODEGRADEIDCONTAINERSIZESOURCEPOSTARGETSTATUSISOVERTOPISREEFCARRYDEVICEPOSONTRUCKISIMDGISOVERFLOWCONTAINERSHORTISOISDISCHECKSEALLISAUTOLISTSEALLNOISDISCHECKSEALCISAUTOLISTSEALCNO58238590979365823859098703IDV9.6NNMRKU23312544DV9C18NF45G1N400300012ECNNN0391MNN

            return XMLParse(rs, new string[] { "WI_WORKITEMS", "DANGERINFO" }, true);
            //return ParseCtos(rs);
        }

        /// <summary>
        /// 根据箱号查装船箱指令信息，（冷超危绑残高，根据CM005001获取）
        /// </summary>
        /// <param name="CNTRSUFFIXLIST"></param>
        /// <param name="VELALIASE"></param>
        /// <param name="WORKPOINTNO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007032(string CNTRSUFFIXLIST, string VELALIASE, string WORKPOINTNO, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format("CNTRSUFFIXLIST:'{0}',VELALIASE:'{1}',WORKPOINTNO:'{2}',TICKET_ID:'{3}'",
                CNTRSUFFIXLIST, VELALIASE, WORKPOINTNO, TICKET_ID);

            string rs = api.OP007032(parms);

            return XMLParse(rs, new string[] { "OP007032" }, false);
        }

        /// <summary>
        /// 确认装船 
        /// </summary>
        /// <param name="DEVICEOPTIMELINESID"></param>
        /// <param name="USER_ID"></param>
        /// <param name="WORKITEMNO"></param>
        /// <param name="QC_DRIVER"></param>
        /// <param name="QC_DEVICENO"></param>
        /// <param name="CONTRACTORCODE"></param>
        /// <param name="BERTHNO"></param>
        /// <param name="ISSHORE"></param>
        /// <param name="DAMCODE"></param>
        /// <param name="CONTAINERNO"></param>
        /// <param name="SOURCEPOS"></param>
        /// <param name="TALLYCLERK"></param>
        /// <param name="TARGET"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult OP007036(string DEVICEOPTIMELINESID, string USER_ID, string WORKITEMNO, string QC_DRIVER, string QC_DEVICENO,
            string CONTRACTORCODE, string BERTHNO, string ISSHORE, string DAMCODE, string CONTAINERNO, string SOURCEPOS,
            string TALLYCLERK, string TARGET, string TICKET_ID)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            string parms = string.Format(@"DEVICEOPTIMELINESID:'{0}',USER_ID:'{1}',WORKITEMNO:'{2}',QC_DRIVER:'{3}',QC_DEVICENO:'{4}',CONTRACTORCODE:'{5}',BERTHNO:'{6}',ISSHORE:'{7}',DAMCODE:'{8}',CONTAINERNO:'{9}',SOURCEPOS:'{10}',TALLYCLERK:'{11}',TARGET:'{12}',TICKET_ID:'{13}'",
                DEVICEOPTIMELINESID, USER_ID, WORKITEMNO, QC_DRIVER, QC_DEVICENO, CONTRACTORCODE, BERTHNO, ISSHORE, DAMCODE, CONTAINERNO,
                SOURCEPOS, TALLYCLERK, TARGET, TICKET_ID);

            string rs = api.OP007036(parms);

            return XMLParse(rs, new string[] { "OP007036" }, false);
        }
/*
        /// <summary>
        /// 确认卸船
        /// </summary>
        /// <param name="VELALIASE"></param>
        /// <param name="DEVICEOPTIMELINESID"></param>
        /// <param name="TICKET_ID"></param>
        /// <param name="WORKITEMNO"></param>
        /// <param name="TYPE"></param>
        /// <param name="STATUS"></param>
        /// <param name="USER_ID"></param>
        /// <param name="QCDRIVER"></param>
        /// <param name="QCNO"></param>
        /// <param name="CONTAINERNO"></param>
        /// <param name="CONTAINERID"></param>
        /// <param name="MAINCONTAINERNO"></param>
        /// <param name="BINDSEQUENCE"></param>
        /// <param name="ISSHORE"></param>
        /// <param name="ISOVERDIS"></param>
        /// <param name="ISFORCE"></param>
        /// <param name="CONTRACTORCODE"></param>
        /// <param name="BERTHNO"></param>
        /// <param name="EMPTYFULL"></param>
        /// <param name="ISOCODE"></param>
        /// <param name="TRUCKNO"></param>
        /// <param name="POSONTRUCK"></param>
        /// <param name="TARGET"></param>
        /// <param name="ISDEALFAILTODECK"></param>
        /// <param name="SEALL"></param>
        /// <param name="SEALC"></param>
        /// <param name="DAM"></param>
        /// <param name="QS"></param>
        /// <param name="IMDG1"></param>
        /// <param name="IMDGSUM1"></param>
        /// <param name="IMDG2"></param>
        /// <param name="IMDGSUM2"></param>
        /// <param name="IMDG3"></param>
        /// <param name="IMDGSUM3"></param>
        /// <param name="ISBAND"></param>
        /// <param name="SETTEMP"></param>
        /// <param name="OH"></param>
        /// <param name="OA"></param>
        /// <param name="OF"></param>
        /// <param name="OL"></param>
        /// <param name="OR"></param>
        /// <param name="DOOR"></param>
        /// <returns></returns>
        public static CtosResult OP007037(string VELALIASE, string DEVICEOPTIMELINESID, string TICKET_ID, string WORKITEMNO, string TYPE, string STATUS,
            string USER_ID, string QCDRIVER, string QCNO, string CONTAINERNO, string CONTAINERID, string MAINCONTAINERNO,
            string BINDSEQUENCE, string ISSHORE, string ISOVERDIS, string ISFORCE,
            string CONTRACTORCODE, string BERTHNO, string EMPTYFULL, string ISOCODE, string TRUCKNO, string POSONTRUCK,
            string TARGET, string ISDEALFAILTODECK, string SEALL, string SEALC, string DAM, string QS,
            string IMDG1, string IMDGSUM1, string IMDG2, string IMDGSUM2, string IMDG3, string IMDGSUM3,
            string ISBAND, string SETTEMP, string OH, string OA, string OF, string OL, string OR, string DOOR)
        {
            CtosWebRef82.WSVCDataAccess api = new OCR.BLL.CtosWebRef82.WSVCDataAccess();
            StringBuilder tableparms = new StringBuilder();

            tableparms.Append(@"WI_WORKITEMBERTHNO:STRINGBINDSEQUENCE:STRINGCONTAINERID:STRINGCONTAINERNO:STRINGCONTRACTORCODE:STRINGDAM:STRINGDOOR:STRINGEMPTYFULL:STRINGIMDG1:STRINGIMDG2:STRINGIMDG3:STRINGIMDGSUM1:STRINGIMDGSUM2:STRINGIMDGSUM3:STRINGISBAND:STRINGISDEALFAILTODECK:STRINGISFORCE:STRINGISOCODE:STRINGISOVERDIS:STRINGISSHORE:STRINGMAINCONTAINERNO:STRINGOA:STRINGOF:STRINGOH:STRINGOL:STRINGOR:STRINGOVER_FIELD:STRINGOVER_VALUE:STRINGPOSONTRUCK:STRINGQCDRIVER:STRINGQCNO:STRINGQS:STRINGSEALC:STRINGSEALL:STRINGSETTEMP:STRINGSTATUS:STRINGTARGET:STRINGTRUCKNO:STRINGTYPE:STRINGUSER_ID:STRINGWORKITEMNO:STRING");

            tableparms.AppendFormat(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}",
                BERTHNO,BINDSEQUENCE,CONTAINERID,CONTAINERNO,CONTRACTORCODE,DAM,DOOR,EMPTYFULL,IMDG1,IMDG2,IMDG3,IMDGSUM1,IMDGSUM2,IMDGSUM3,
                ISBAND,ISDEALFAILTODECK,ISFORCE,ISOCODE,ISOVERDIS,ISSHORE,MAINCONTAINERNO,OA,OF,OH,OL,OR,string.Empty,string.Empty,POSONTRUCK,
                QCDRIVER,QCNO,QS,SEALC,SEALL,SETTEMP,STATUS,TARGET,TRUCKNO,TYPE,USER_ID,WORKITEMNO);
            tableparms.Append(@"");

            string parmsBase64 = EncodeBase64(Encoding.UTF8, tableparms.ToString());
   
            string SESSIONGUID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string parms = string.Format(@"DEVICEOPTIMELINESID:'{0}',SESSIONGUID:'{1}',TABLEPARAMS:'{2}',VELALIASE:'{3}',",
                DEVICEOPTIMELINESID, SESSIONGUID, parmsBase64, VELALIASE);
            string parms2Base64 = EncodeBase64(Encoding.UTF8, parms);

            byte[] bytes = api.ExecBiz("OP007037", parms2Base64, TICKET_ID);
            string rs = Encoding.UTF8.GetString(bytes);

            return ParseCtos(rs);

        }

        /// <summary>
        /// 确认卸船
        /// </summary>
        /// <param name="VELALIASE"></param>
        /// <param name="DEVICEOPTIMELINESID"></param>
        /// <param name="TICKET_ID"></param>
        /// <param name="WORKITEMNO"></param>
        /// <param name="TYPE"></param>
        /// <param name="STATUS"></param>
        /// <param name="USER_ID"></param>
        /// <param name="QCDRIVER"></param>
        /// <param name="QCNO"></param>
        /// <param name="CONTAINERNO"></param>
        /// <param name="CONTAINERID"></param>
        /// <param name="MAINCONTAINERNO"></param>
        /// <param name="BINDSEQUENCE"></param>
        /// <param name="ISSHORE"></param>
        /// <param name="ISOVERDIS"></param>
        /// <param name="ISFORCE"></param>
        /// <param name="CONTRACTORCODE"></param>
        /// <param name="BERTHNO"></param>
        /// <param name="EMPTYFULL"></param>
        /// <param name="ISOCODE"></param>
        /// <param name="TRUCKNO"></param>
        /// <param name="POSONTRUCK"></param>
        /// <param name="TARGET"></param>
        /// <param name="ISDEALFAILTODECK"></param>
        /// <param name="SEALL"></param>
        /// <param name="SEALC"></param>
        /// <param name="DAM"></param>
        /// <param name="QS"></param>
        /// <param name="IMDG1"></param>
        /// <param name="IMDGSUM1"></param>
        /// <param name="IMDG2"></param>
        /// <param name="IMDGSUM2"></param>
        /// <param name="IMDG3"></param>
        /// <param name="IMDGSUM3"></param>
        /// <param name="ISBAND"></param>
        /// <param name="SETTEMP"></param>
        /// <param name="OH"></param>
        /// <param name="OA"></param>
        /// <param name="OF"></param>
        /// <param name="OL"></param>
        /// <param name="OR"></param>
        /// <param name="DOOR"></param>
        /// <returns></returns>
        public static CtosResult OP007037B(string VELALIASE, string DEVICEOPTIMELINESID, string TICKET_ID, string WORKITEMNO, string TYPE, string STATUS,
            string USER_ID, string QCDRIVER, string QCNO, string CONTAINERNO, string CONTAINERID, string MAINCONTAINERNO,
            string BINDSEQUENCE, string ISSHORE, string ISOVERDIS, string ISFORCE,
            string CONTRACTORCODE, string BERTHNO, string EMPTYFULL, string ISOCODE, string TRUCKNO, string POSONTRUCK,
            string TARGET, string ISDEALFAILTODECK, string SEALL, string SEALC, string DAM, string QS,
            string IMDG1, string IMDGSUM1, string IMDG2, string IMDGSUM2, string IMDG3, string IMDGSUM3,
            string ISBAND, string SETTEMP, string OH, string OA, string OF, string OL, string OR, string DOOR)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            StringBuilder tableparms = new StringBuilder();

            tableparms.Append(@"WI_WORKITEMBERTHNO:STRINGBINDSEQUENCE:STRINGCONTAINERID:STRINGCONTAINERNO:STRINGCONTRACTORCODE:STRINGDAM:STRINGDOOR:STRINGEMPTYFULL:STRINGIMDG1:STRINGIMDG2:STRINGIMDG3:STRINGIMDGSUM1:STRINGIMDGSUM2:STRINGIMDGSUM3:STRINGISBAND:STRINGISDEALFAILTODECK:STRINGISFORCE:STRINGISOCODE:STRINGISOVERDIS:STRINGISSHORE:STRINGMAINCONTAINERNO:STRINGOA:STRINGOF:STRINGOH:STRINGOL:STRINGOR:STRINGPOSONTRUCK:STRINGQCDRIVER:STRINGQCNO:STRINGQS:STRINGSEALC:STRINGSEALL:STRINGSETTEMP:STRINGSTATUS:STRINGTARGET:STRINGTRUCKNO:STRINGTYPE:STRINGUSER_ID:STRINGWORKITEMNO:STRING");

            tableparms.AppendFormat(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}",
                BERTHNO, BINDSEQUENCE, CONTAINERID, CONTAINERNO, CONTRACTORCODE, DAM, DOOR, EMPTYFULL, IMDG1, IMDG2, IMDG3, IMDGSUM1, IMDGSUM2, IMDGSUM3,
                ISBAND, ISDEALFAILTODECK, ISFORCE, ISOCODE, ISOVERDIS, ISSHORE, MAINCONTAINERNO, OA, OF, OH, OL, OR, POSONTRUCK,
                QCDRIVER, QCNO, QS, SEALC, SEALL, SETTEMP, STATUS, TARGET, TRUCKNO, TYPE, USER_ID, WORKITEMNO);

    //        tableparms.AppendFormat(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}",
    //BERTHNO, BINDSEQUENCE, "5823865229661", "ABCU1111132", CONTRACTORCODE, DAM, DOOR, EMPTYFULL, IMDG1, IMDG2, IMDG3, IMDGSUM1, IMDGSUM2, IMDGSUM3,
    //ISBAND, ISDEALFAILTODECK, ISFORCE, ISOCODE, ISOVERDIS, ISSHORE, MAINCONTAINERNO, OA, OF, OH, OL, OR, "1A",
    //QCDRIVER, QCNO, QS, SEALC, SEALL, SETTEMP, STATUS, TARGET, TRUCKNO, TYPE, USER_ID, "5823865229726");

            tableparms.Append(@"");

            string parmsBase64 = EncodeBase64(Encoding.UTF8, tableparms.ToString());

            string parms = string.Format(@"VELALIASE:'{0}',DEVICEOPTIMELINESID:'{1}',TABLEPARAMS:'{2}',TICKET_ID:'{3}'",
                VELALIASE, DEVICEOPTIMELINESID, parmsBase64, TICKET_ID);

            string rs = api.OP007037(parms);

            return XMLParse(rs, new string[] { "OP007037" }, false);
        }

*/
        public static CtosResult OP007037C(string VELALIASE, string DEVICEOPTIMELINESID, string TICKET_ID, string USER_ID, string QCDRIVER, string QCNO,
            string CONTRACTORCODE, string BERTHNO, string MAINCONTAINERNO, OcrCnt.T_OCR_CNTRow row1, OcrCnt.T_OCR_CNTRow row2)
        {
            CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            StringBuilder tableparms = new StringBuilder();

            tableparms.Append(@"WI_WORKITEMBERTHNO:STRINGBINDSEQUENCE:STRINGCONTAINERID:STRINGCONTAINERNO:STRINGCONTRACTORCODE:STRINGDAM:STRINGDOOR:STRINGEMPTYFULL:STRINGIMDG1:STRINGIMDG2:STRINGIMDG3:STRINGIMDGSUM1:STRINGIMDGSUM2:STRINGIMDGSUM3:STRINGISBAND:STRINGISDEALFAILTODECK:STRINGISFORCE:STRINGISOCODE:STRINGISOVERDIS:STRINGISSHORE:STRINGMAINCONTAINERNO:STRINGOA:STRINGOF:STRINGOH:STRINGOL:STRINGOR:STRINGPOSONTRUCK:STRINGQCDRIVER:STRINGQCNO:STRINGQS:STRINGSEALC:STRINGSEALL:STRINGSETTEMP:STRINGSTATUS:STRINGTARGET:STRINGTRUCKNO:STRINGTYPE:STRINGUSER_ID:STRINGWORKITEMNO:STRING");

            tableparms.AppendFormat(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}",
            BERTHNO, row1.BNDL, row1.CONTAINERID, row1.cntNo, CONTRACTORCODE, row1.Dmg, row1.door, row1.emptyFull, row1.Imdg1, row1.Imdg2, row1.Imdg3, row1.ImdgNum1, row1.ImdgNum2, row1.ImdgNum3,
            row1.ISBIND, "Y", "N", row1.iso, row1.overDis, row1.isShore, MAINCONTAINERNO, row1.OA, row1.OF, row1.OH, row1.OL, row1.OR, row1.positionTruck,
            QCDRIVER, QCNO, row1.QS, row1.CIQSEALNO, row1.VESSELCOMPANYSEALNO, row1.SETUPTEMP, row1.WORKITEMSTATUS, row1.TARGET, row1.truckno, "C", USER_ID, row1.WORKITEMNO);

            if (row2 != null)
            {
                tableparms.AppendFormat(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}",
            BERTHNO, row2.BNDL, row2.CONTAINERID, row2.cntNo, CONTRACTORCODE, row2.Dmg, row2.door, row2.emptyFull, row2.Imdg1, row2.Imdg2, row2.Imdg3, row2.ImdgNum1, row2.ImdgNum2, row2.ImdgNum3,
            row2.ISBIND, "Y", "N", row2.iso, row2.overDis, row2.isShore, MAINCONTAINERNO, row2.OA, row2.OF, row2.OH, row2.OL, row2.OR, row2.positionTruck,
            QCDRIVER, QCNO, row2.QS, row2.CIQSEALNO, row2.VESSELCOMPANYSEALNO, row2.SETUPTEMP, row2.WORKITEMSTATUS, row2.TARGET, row2.truckno, "C", USER_ID, row2.WORKITEMNO);

            }


            //        tableparms.AppendFormat(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}",
            //BERTHNO, BINDSEQUENCE, "5823865229661", "ABCU1111132", CONTRACTORCODE, DAM, DOOR, EMPTYFULL, IMDG1, IMDG2, IMDG3, IMDGSUM1, IMDGSUM2, IMDGSUM3,
            //ISBAND, ISDEALFAILTODECK, ISFORCE, ISOCODE, ISOVERDIS, ISSHORE, MAINCONTAINERNO, OA, OF, OH, OL, OR, "1A",
            //QCDRIVER, QCNO, QS, SEALC, SEALL, SETTEMP, STATUS, TARGET, TRUCKNO, TYPE, USER_ID, "5823865229726");

            tableparms.Append(@"");

            string parmsBase64 = EncodeBase64(Encoding.UTF8, tableparms.ToString());

            string parms = string.Format(@"VELALIASE:'{0}',TWINLIFT:'{1}',DEVICEOPTIMELINESID:'{2}',TABLEPARAMS:'{3}',TICKET_ID:'{4}'",
                VELALIASE, row1.CTYPE == 1 ? "Y" : "N", DEVICEOPTIMELINESID, parmsBase64, TICKET_ID);

            string rs = api.OP007037(parms);

            return XMLParse(rs, new string[] { "OP007037" }, false);
        }

        /// <summary>
        /// 根据箱号查找箱
        /// </summary>
        /// <param name="CONTAINERNO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        public static CtosResult CM005001(string CONTAINERNO, string TICKET_ID)
        {
            //CtosWebReference.Service api = new OCR.BLL.CtosWebReference.Service();
            //string parms = string.Format("CONTAINERNO:'{0}',TICKET_ID:'{1}'", CONTAINERNO, TICKET_ID);

            //string rs = api.CM005001(parms);

            //return XMLParse(rs, new string[] { "CM_CONTAINERS", "CM_CONTAINERS_BINDINFO", "CM_CONTAINERIMDGINFO", "CM_CONTAINERDANGERINFO" }, true);
            return ctosDAL.CM005001(CONTAINERNO);
        }

        /// <summary>
        /// 修改残损信息
        /// </summary>
        /// <param name="CONTAINERID"></param>
        /// <param name="DAMAGECODE"></param>
        /// <param name="DAMAGEEMEMO"></param>
        /// <param name="TICKET_ID"></param>
        /// <returns></returns>
        /// <remarks>如果箱已经压车，调用接口时，会根据堆场计划重新找场位</remarks>
        public static CtosResult OP007095(string CONTAINERID, string DAMAGECODE, string DAMAGEEMEMO, string TICKET_ID)
        {
            WebReferenceTest.Service api2 = new OCR.BLL.WebReferenceTest.Service();
            StringBuilder tableparms = new StringBuilder();

            tableparms.Append(@"WI_WORKITEMDAMAGECODE:STRINGDAMAGEEMEMO:STRING");

            tableparms.AppendFormat(@"{0}{1}", DAMAGECODE, DAMAGEEMEMO);

            tableparms.Append(@"");

            string parmsBase64 = EncodeBase64(Encoding.UTF8, tableparms.ToString());

            string parms = string.Format(@"CONTAINERID:'{0}',TABLEPARAMS:'{1}',TICKET_ID:'{2}'",
                CONTAINERID, parmsBase64, TICKET_ID);

            string rs = api2.OP007095(parms);

            return XMLParse(rs, new string[] { "OP007095" }, false);

        }

        /// <summary>
        /// 解XML文本
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="methodName"></param>
        /// <param name="isDataset"></param>
        /// <returns></returns>
        public static CtosResult XMLParse(string xml, string[] methodName, bool isDataset)
        {
            CtosResult rs = new CtosResult();

            XmlDocument xx = new XmlDocument();
            xx.LoadXml(xml);
            XmlNode errorCode = xx.SelectSingleNode("/CTOSRESULT/RETURNINFO/ERRORCODE");
            rs.ERRORCODE = errorCode.InnerText;
            XmlNode errorMsg = xx.SelectSingleNode("/CTOSRESULT/RETURNINFO/ERRORMSG");
            rs.ERRORMSG = errorMsg.InnerText;

            //成功
            if (rs.ERRORCODE == SUCCESSCODE)
            {
                foreach (string name1 in methodName)
                {
                    if (isDataset)
                    {
                        XmlNodeList nodes = xx.SelectNodes("/CTOSRESULT/" + name1);
                        DataTable dt = new DataTable(name1);
                        rs.DS.Tables.Add(dt);

                        if (nodes != null)
                        {
                            int i = 0;
                            foreach (XmlNode node1 in nodes)
                            {
                                if (i == 0)
                                {
                                    foreach (XmlNode node2 in node1.ChildNodes)
                                    {   //创建列
                                        rs.DS.Tables[name1].Columns.Add(new DataColumn(node2.Name, typeof(string)));
                                    }
                                }

                                //创建行
                                DataRow dr = rs.DS.Tables[name1].NewRow();
                                foreach (XmlNode node2 in node1.ChildNodes)
                                {
                                    dr[node2.Name] = node2.InnerText;
                                }
                                rs.DS.Tables[name1].Rows.Add(dr);

                                i++;
                            }
                        }
                    }
                    else
                    {
                        XmlNode node1 = xx.SelectSingleNode("/CTOSRESULT/" + name1);
                        if (node1 != null)
                        {
                            foreach (XmlNode node2 in node1.ChildNodes)
                            {
                                rs.DIC.Add(node2.Name, node2.InnerText);
                            }
                        }
                    }
                }
            }

            return rs;

            //XmlNodeList xxList = xx.GetElementsByTagName("row"); //取得节点名为row的XmlNode集合
            //foreach (XmlNode xxNode in xxList)
            //{
            //    XmlNodeList childList = xxNode.ChildNodes; //取得row下的子节点集合
            //    foreach (XmlNode xxNode in xxList)
            //    {
            //        xxNode.InnerText; //返回的是col的文字内容
            //        xxNode.Attributes["name"].Value; //col节点name属性值
            //    }
            //}
        }

        /// <summary>
        /// 解CTOS特定格式返回值
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static CtosResult ParseCtos(string text)
        {
            //string str = @"OP0070310WI_WORKITEMSCONTAINERIDWORKITEMNOINAIMCONTAINERTYPECONTAINERHEIGHTISDISCHECKCTNISDISCHECKSEALSPECIALREQUIREMENTSCONTAINERNOSHORTCODEWORKPOINTNOSETUPTEMPTEMPTYPEVESSELCOMPANYSEALNOVESSELCOMPANYSEALNOLISTCIQSEALNOCIQSEALNOLISTISAUTOLISTSEALNOOVERFRONTOVERBEHINDOVERLEFTOVERRIGHTOVERTOPEMPTYFULLISOCODEISBINDBINDSEQUENCEDAMAGECODEGRADEIDCONTAINERSIZESOURCEPOSTARGETSTATUSISOVERTOPISREEFCARRYDEVICEPOSONTRUCKISIMDGISOVERFLOWCONTAINERSHORTISOISDISCHECKSEALLISAUTOLISTSEALLNOISDISCHECKSEALCISAUTOLISTSEALCNO58238590979365823859098703IDV9.6NNMRKU23312544DV9C18NF45G1N400300012ECNNN0391MNN"
            string[] array = text.Split(new char[] { '' });
            CtosResult rs = new CtosResult();
            rs.ERRORCODE = array[1];
            string[] a1 = array[2].Split(new char[] { '' });
            rs.ERRORMSG = a1[0];

            //成功
            if (rs.ERRORCODE == SUCCESSCODE && array.Length > 3)
            {
                int tableNameIndex = 0;
                bool isCol = true;
                int colIndex = 0;
                int arrayIndex = 0;
                foreach (string str1 in array)
                {
                    if (arrayIndex < 2)
                    {
                        arrayIndex++;
                        continue;
                    }


                    if (str1.Contains(""))
                    {
                        isCol = true;
                        string tableName = str1.Substring(str1.IndexOf("") + 1, str1.IndexOf("") - 1);
                        DataTable dt = new DataTable(tableName);
                        rs.DS.Tables.Add(dt);
                        tableNameIndex++;

                        rs.DS.Tables[rs.DS.Tables.Count - 1].Columns.Add(new DataColumn(str1.Substring(str1.IndexOf("") + 1, str1.Length - str1.IndexOf("") - 1), typeof(string)));
                    }
                    else if (str1.EndsWith(""))
                    {
                        rs.DS.Tables[rs.DS.Tables.Count - 1].Rows[rs.DS.Tables[rs.DS.Tables.Count - 1].Rows.Count - 1][colIndex] = str1.Remove(str1.Length - 1, 1);

                    }
                    else if (str1.Contains(""))
                    {
                        //YDANGERINFOIMDGUNNO
                        string[] a2 = str1.Split(new char[] { '' });
                        rs.DS.Tables[rs.DS.Tables.Count - 1].Rows[rs.DS.Tables[rs.DS.Tables.Count - 1].Rows.Count - 1][colIndex] = a2[0];

                        string[] a3 = a2[1].Split(new char[] { '' });
                        isCol = true;
                        string tableName = a3[0];
                        DataTable dt = new DataTable(tableName);
                        rs.DS.Tables.Add(dt);
                        tableNameIndex++;

                        rs.DS.Tables[rs.DS.Tables.Count - 1].Columns.Add(new DataColumn(a3[1], typeof(string)));
                        colIndex = 0;
                    }
                    else if (str1.Contains(""))
                    {
                        string[] a2 = str1.Split(new char[] { '' });
                        rs.DS.Tables[rs.DS.Tables.Count - 1].Columns.Add(new DataColumn(a2[0], typeof(string)));

                        isCol = false;
                        DataRow dr = rs.DS.Tables[rs.DS.Tables.Count - 1].NewRow();
                        rs.DS.Tables[rs.DS.Tables.Count - 1].Rows.Add(dr);

                        rs.DS.Tables[rs.DS.Tables.Count - 1].Rows[rs.DS.Tables[rs.DS.Tables.Count - 1].Rows.Count - 1][colIndex] = a2[1];
                        colIndex++;
                    }
                    else if (isCol)
                    {
                        rs.DS.Tables[rs.DS.Tables.Count - 1].Columns.Add(new DataColumn(str1, typeof(string)));
                    }
                    else if (!isCol)
                    {
                        rs.DS.Tables[rs.DS.Tables.Count - 1].Rows[rs.DS.Tables[rs.DS.Tables.Count - 1].Rows.Count - 1][colIndex] = str1;
                        colIndex++;
                    }

                }
               
            }
            return rs;
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="codeName">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch
            {
               
            }
            return source;
        }


        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">加密采用的编码方式</param>
        /// <param name="source">待解密的明文</param>
        /// <returns></returns>
        public static string DecodeBase64(Encoding encode, string source)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(source);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = source;
            }
            return decode;
        }
    }
}
