-------------------------------------------
-- Export file for user OCR              --
-- Created by yejt on 2018/1/2, 12:23:02 --
-------------------------------------------

spool ��20180102.log

prompt
prompt Creating table T_OCRX_BARGE
prompt ===========================
prompt
create table OCR.T_OCRX_BARGE
(
  COMPANYCODE     VARCHAR2(10),
  CREATEDBY       VARCHAR2(50),
  CREATETIME      DATE default sysdate,
  UPDATEDBY       VARCHAR2(50),
  UPDATETIME      DATE default sysdate,
  BERTHPLANNO     NUMBER(38) not null,
  SHIP_CODE       VARCHAR2(64),
  IN_VOYAGE_CODE  VARCHAR2(64),
  OUT_VOYAGE_CODE VARCHAR2(64),
  ENAME           VARCHAR2(100)
)
;
comment on table OCR.T_OCRX_BARGE
  is '�����ַ�����';
comment on column OCR.T_OCRX_BARGE.COMPANYCODE
  is '����˾����';
comment on column OCR.T_OCRX_BARGE.CREATEDBY
  is '������';
comment on column OCR.T_OCRX_BARGE.CREATETIME
  is '����ʱ��';
comment on column OCR.T_OCRX_BARGE.UPDATEDBY
  is '������';
comment on column OCR.T_OCRX_BARGE.UPDATETIME
  is '����ʱ��';
comment on column OCR.T_OCRX_BARGE.BERTHPLANNO
  is '����ID';
comment on column OCR.T_OCRX_BARGE.SHIP_CODE
  is '��������';
comment on column OCR.T_OCRX_BARGE.IN_VOYAGE_CODE
  is '���ں���';
comment on column OCR.T_OCRX_BARGE.OUT_VOYAGE_CODE
  is '���ں���';
comment on column OCR.T_OCRX_BARGE.ENAME
  is '������';
alter table OCR.T_OCRX_BARGE
  add constraint PK_T_OCRX_BARGE primary key (BERTHPLANNO);
create index OCR.IX_T_OCRX_BARGE on OCR.T_OCRX_BARGE (ENAME);

prompt
prompt Creating table T_OCRX_CNT
prompt =========================
prompt
create table OCR.T_OCRX_CNT
(
  DOCK_ID          NUMBER(20) not null,
  ISARCHIVED       VARCHAR2(1) default 'N' not null,
  CONTAINER_NO     VARCHAR2(20),
  RCONTAINER_NO    VARCHAR2(20),
  CONTAINER_SHAPE  VARCHAR2(20),
  RCONTAINER_SHAPE VARCHAR2(20),
  CONTAINER_SIZE   NUMBER(4),
  CTYPE            NUMBER(1),
  DOCK_STATUS      NUMBER(1),
  CSTATUS          NUMBER(12),
  TRVALCRANE_ID    NUMBER(10),
  TRVAL_NO         VARCHAR2(8),
  CNAM             VARCHAR2(64),
  ENAM             VARCHAR2(64),
  SHIP_CODE        VARCHAR2(64),
  C_VOYAGE         VARCHAR2(40),
  PIC_NUM          NUMBER(4),
  MSG_INDEX        NUMBER(20),
  CTIME            DATE,
  PMS_ID           NUMBER(8),
  COPYTIME         DATE default sysdate,
  STARTTIME        DATE,
  FINISHTIME       DATE,
  OPERATORNAME     VARCHAR2(50),
  BERTH_NUM        VARCHAR2(8),
  CONTAINERID      INTEGER,
  CTOSERRORMSG     VARCHAR2(500),
  CTOSERRORCODE    VARCHAR2(50),
  EXCEPSTARTIME    DATE,
  EXCEPFINISHTIME  DATE,
  EXCEPUSER        VARCHAR2(50),
  LINECODE         VARCHAR2(50),
  COMPANYCODE      VARCHAR2(20),
  SERVICECODE      VARCHAR2(20),
  SHIPAGENT        VARCHAR2(10),
  SHIPOWNER        VARCHAR2(18),
  ISBIND           VARCHAR2(1),
  MAINCONTAINERNO  VARCHAR2(12),
  BINDSEQ          NUMBER(2),
  ISDAMAGE         VARCHAR2(1),
  DMG              VARCHAR2(200),
  ISOVERTOP        VARCHAR2(1),
  OVR              NUMBER(10,2),
  OVL              NUMBER(10,2),
  OVH              NUMBER(10,2),
  OVF              NUMBER(10,2),
  OVA              NUMBER(10,2),
  ISOVERDIS        VARCHAR2(1),
  ISIMDG           VARCHAR2(1),
  IMDG1            VARCHAR2(4),
  IMDG2            VARCHAR2(4),
  IMDG3            VARCHAR2(4),
  EMPTYFULL        VARCHAR2(1),
  CONTAINERTYPE    VARCHAR2(2),
  CONTAINER_HEIGHT NUMBER(3,1),
  INAIM            VARCHAR2(1)
)
partition by list (ISARCHIVED)
(
  partition PCUR values ('N')
    tablespace CCTCCTDB,
  partition PHIS values ('Y')
    tablespace CCTCCTDB,
  partition PARC values ('A')
    tablespace CCTCCTDB
);
comment on table OCR.T_OCRX_CNT
  is '����װж��ű�';
comment on column OCR.T_OCRX_CNT.DOCK_ID
  is '��װ��ID';
comment on column OCR.T_OCRX_CNT.ISARCHIVED
  is '�Ƿ�鵵,''N''��ǰ����/Y��ʱ��ʷ���� /A�鵵����';
comment on column OCR.T_OCRX_CNT.CONTAINER_NO
  is '�ն�ʶ����ţ�δʶ�������Ϊ��δʶ��';
comment on column OCR.T_OCRX_CNT.RCONTAINER_NO
  is '��¼�޸��Ժ�����,����ͳ��ʶ����';
comment on column OCR.T_OCRX_CNT.CONTAINER_SHAPE
  is '���ͣ���Ӧ�ն�ʶ��ISO';
comment on column OCR.T_OCRX_CNT.RCONTAINER_SHAPE
  is '�޸ĺ����ͣ�����ͳ��ʶ����';
comment on column OCR.T_OCRX_CNT.CONTAINER_SIZE
  is '��ߴ磬����40��С��20';
comment on column OCR.T_OCRX_CNT.CTYPE
  is '�ж�ʶ��˫�䣬0Ϊ���䣬1Ϊ˫��';
comment on column OCR.T_OCRX_CNT.DOCK_STATUS
  is 'װж״̬��0Ϊװ����1Ϊж�����ն�д��';
comment on column OCR.T_OCRX_CNT.CSTATUS
  is '���Ӵ���״̬��-1/װ��δ�Զ�����, 0/׼���˹�����,1/���˹���ȡ,2/�ѳɹ����� 3/ת�쳣 4/�쳣�������';
comment on column OCR.T_OCRX_CNT.TRVALCRANE_ID
  is '�ŵ�ID��ƽ̨���ú��ŵ���Ӧ�ն˺󣬶�Ӧ�ն˵����ݻ�����ŵ�ID�����ڱ�ʶ�������ĸ��ŵ�����';
comment on column OCR.T_OCRX_CNT.TRVAL_NO
  is '�ŵ���';
comment on column OCR.T_OCRX_CNT.CNAM
  is '���Ĵ���';
comment on column OCR.T_OCRX_CNT.ENAM
  is '������';
comment on column OCR.T_OCRX_CNT.SHIP_CODE
  is '��������';
comment on column OCR.T_OCRX_CNT.C_VOYAGE
  is '����';
comment on column OCR.T_OCRX_CNT.PIC_NUM
  is '���Ӷ�ӦͼƬ����';
comment on column OCR.T_OCRX_CNT.MSG_INDEX
  is '��Ϣid���ն��ϴ���˫��������������ͬ';
comment on column OCR.T_OCRX_CNT.CTIME
  is '����ʱ�� --OCR';
comment on column OCR.T_OCRX_CNT.PMS_ID
  is 'ͼƬ������Id';
comment on column OCR.T_OCRX_CNT.COPYTIME
  is 'ͬ������ʱ��';
comment on column OCR.T_OCRX_CNT.STARTTIME
  is '����Ա��ȡͼƬʱ��';
comment on column OCR.T_OCRX_CNT.FINISHTIME
  is '����Ա���ͼƬ����ʱ��';
comment on column OCR.T_OCRX_CNT.OPERATORNAME
  is '����Ա';
comment on column OCR.T_OCRX_CNT.BERTH_NUM
  is '��λ��';
comment on column OCR.T_OCRX_CNT.CONTAINERID
  is '��ID';
comment on column OCR.T_OCRX_CNT.CTOSERRORMSG
  is 'CTOS������Ϣ';
comment on column OCR.T_OCRX_CNT.CTOSERRORCODE
  is 'CTOS�������';
comment on column OCR.T_OCRX_CNT.EXCEPSTARTIME
  is '�쳣����ʼʱ��';
comment on column OCR.T_OCRX_CNT.EXCEPFINISHTIME
  is '�쳣������ʱ��';
comment on column OCR.T_OCRX_CNT.EXCEPUSER
  is '�쳣������';
comment on column OCR.T_OCRX_CNT.LINECODE
  is '����';
comment on column OCR.T_OCRX_CNT.COMPANYCODE
  is '����˾����';
comment on column OCR.T_OCRX_CNT.SERVICECODE
  is '���ߴ���';
comment on column OCR.T_OCRX_CNT.SHIPAGENT
  is '����';
comment on column OCR.T_OCRX_CNT.SHIPOWNER
  is '����';
comment on column OCR.T_OCRX_CNT.ISBIND
  is '�Ƿ������';
comment on column OCR.T_OCRX_CNT.MAINCONTAINERNO
  is '�����';
comment on column OCR.T_OCRX_CNT.BINDSEQ
  is '����˳��';
comment on column OCR.T_OCRX_CNT.ISDAMAGE
  is '�Ƿ����';
comment on column OCR.T_OCRX_CNT.DMG
  is '����˵��';
comment on column OCR.T_OCRX_CNT.ISOVERTOP
  is '�Ƿ�����';
comment on column OCR.T_OCRX_CNT.OVR
  is '�ҳ�';
comment on column OCR.T_OCRX_CNT.OVL
  is '��';
comment on column OCR.T_OCRX_CNT.OVH
  is '����';
comment on column OCR.T_OCRX_CNT.OVF
  is 'ǰ��';
comment on column OCR.T_OCRX_CNT.OVA
  is '��';
comment on column OCR.T_OCRX_CNT.ISOVERDIS
  is '�Ƿ���ж';
comment on column OCR.T_OCRX_CNT.ISIMDG
  is '�Ƿ�Σ��';
comment on column OCR.T_OCRX_CNT.IMDG1
  is 'Σ��ȼ�1';
comment on column OCR.T_OCRX_CNT.IMDG2
  is 'Σ��ȼ�2';
comment on column OCR.T_OCRX_CNT.IMDG3
  is 'Σ��ȼ�3';
comment on column OCR.T_OCRX_CNT.EMPTYFULL
  is '����';
comment on column OCR.T_OCRX_CNT.CONTAINERTYPE
  is '����';
comment on column OCR.T_OCRX_CNT.CONTAINER_HEIGHT
  is '���';
comment on column OCR.T_OCRX_CNT.INAIM
  is '��������';
alter table OCR.T_OCRX_CNT
  add constraint PK_T_OCR_CNTX primary key (DOCK_ID);
create index OCR.IX_T_OCRX_CNT2 on OCR.T_OCRX_CNT (CTIME);
create index OCR.IX_T_OCRX_CNT3 on OCR.T_OCRX_CNT (ENAM);
create index OCR.IX_T_OCRX_CNT4 on OCR.T_OCRX_CNT (CONTAINER_NO, RCONTAINER_NO);
create index OCR.IX_T_OCR_CNTX on OCR.T_OCRX_CNT (MSG_INDEX, TRVAL_NO);

prompt
prompt Creating table T_OCRX_CO
prompt ========================
prompt
create table OCR.T_OCRX_CO
(
  COMPANYCODE VARCHAR2(10) not null,
  COMPANYNAME VARCHAR2(100),
  CREATEDBY   VARCHAR2(50),
  CREATETIME  DATE default sysdate,
  UPDATEDBY   VARCHAR2(50),
  UPDATETIME  DATE default sysdate
)
;
comment on table OCR.T_OCRX_CO
  is '����˾����';
comment on column OCR.T_OCRX_CO.COMPANYCODE
  is '����˾����';
comment on column OCR.T_OCRX_CO.COMPANYNAME
  is '����˾����';
comment on column OCR.T_OCRX_CO.CREATEDBY
  is '������';
comment on column OCR.T_OCRX_CO.CREATETIME
  is '����ʱ��';
comment on column OCR.T_OCRX_CO.UPDATEDBY
  is '������';
comment on column OCR.T_OCRX_CO.UPDATETIME
  is '����ʱ��';
alter table OCR.T_OCRX_CO
  add constraint PK_T_OCRX_CO primary key (COMPANYCODE);

prompt
prompt Creating table T_OCRX_USERS
prompt ===========================
prompt
create table OCR.T_OCRX_USERS
(
  USERID      VARCHAR2(50) not null,
  COMPANYCODE VARCHAR2(10),
  CREATEDBY   VARCHAR2(50),
  CREATETIME  DATE default sysdate,
  UPDATEDBY   VARCHAR2(50),
  UPDATETIME  DATE default sysdate
)
;
comment on table OCR.T_OCRX_USERS
  is '����˾�û���ϵ';
comment on column OCR.T_OCRX_USERS.USERID
  is '�û�ID';
comment on column OCR.T_OCRX_USERS.COMPANYCODE
  is '����˾����';
comment on column OCR.T_OCRX_USERS.CREATEDBY
  is '������';
comment on column OCR.T_OCRX_USERS.CREATETIME
  is '����ʱ��';
comment on column OCR.T_OCRX_USERS.UPDATEDBY
  is '������';
comment on column OCR.T_OCRX_USERS.UPDATETIME
  is '����ʱ��';
alter table OCR.T_OCRX_USERS
  add constraint PK_T_OCRX_USERS primary key (USERID);

prompt
prompt Creating table T_OCRX_VESSEL
prompt ============================
prompt
create table OCR.T_OCRX_VESSEL
(
  COMPANYCODE VARCHAR2(10) not null,
  SERVICECODE VARCHAR2(20) not null,
  LINECODE    VARCHAR2(200),
  CREATEDBY   VARCHAR2(50),
  CREATETIME  DATE default sysdate,
  UPDATEDBY   VARCHAR2(50),
  UPDATETIME  DATE default sysdate
)
;
comment on table OCR.T_OCRX_VESSEL
  is '�������ݷַ�����';
comment on column OCR.T_OCRX_VESSEL.COMPANYCODE
  is '����˾����';
comment on column OCR.T_OCRX_VESSEL.SERVICECODE
  is '���ߴ���';
comment on column OCR.T_OCRX_VESSEL.LINECODE
  is '�����б�';
comment on column OCR.T_OCRX_VESSEL.CREATEDBY
  is '������';
comment on column OCR.T_OCRX_VESSEL.CREATETIME
  is '����ʱ��';
comment on column OCR.T_OCRX_VESSEL.UPDATEDBY
  is '������';
comment on column OCR.T_OCRX_VESSEL.UPDATETIME
  is '����ʱ��';
alter table OCR.T_OCRX_VESSEL
  add constraint PK_T_OCRX_VESSEL primary key (COMPANYCODE, SERVICECODE);

prompt
prompt Creating table T_OCR_CNT
prompt ========================
prompt
create table OCR.T_OCR_CNT
(
  DOCK_ID             NUMBER(20) not null,
  ISARCHIVED          VARCHAR2(1) default 'N',
  CONTAINER_NO        VARCHAR2(20),
  RCONTAINER_NO       VARCHAR2(20),
  CONTAINER_SHAPE     VARCHAR2(20),
  RCONTAINER_SHAPE    VARCHAR2(20),
  CONTAINER_SIZE      NUMBER(4),
  CONTAINER_HEIGHT    NUMBER(4),
  PLATE_NO            VARCHAR2(16),
  RPALGE_NO           VARCHAR2(16),
  CTYPE               NUMBER(1),
  CWEIGT              NUMBER(8,3),
  DOCK_STATUS         NUMBER(1),
  CONTAINER_POS       VARCHAR2(16),
  CSTATUS             NUMBER(12),
  CONFIDENCE          NUMBER(4),
  TRVALCRANE_ID       NUMBER(10),
  TRVAL_NO            VARCHAR2(8),
  DRIVER_NO           VARCHAR2(32),
  LANE_NO             NUMBER(4),
  BEGIN_TIME          DATE,
  END_TIME            DATE,
  USER_ID             VARCHAR2(20),
  CNAM                VARCHAR2(64),
  ENAM                VARCHAR2(64),
  SHIP_CODE           VARCHAR2(64),
  C_VOYAGE            VARCHAR2(40),
  PIC_NUM             NUMBER(4),
  MSG_INDEX           NUMBER(20),
  CARCONT             NUMBER(2),
  GANGS               VARCHAR2(32),
  CONTAINER_DIR       VARCHAR2(32),
  STREAM_DIR          VARCHAR2(32),
  LOADING_PORT        VARCHAR2(64),
  UNLOADING_PORT      VARCHAR2(64),
  DEST_PORT           VARCHAR2(64),
  BAY_HORIZONTAL      VARCHAR2(16),
  BAY_VERTICAL        VARCHAR2(16),
  BAY                 VARCHAR2(8),
  CTIME               DATE,
  PMS_ID              NUMBER(8),
  COPYTIME            DATE default sysdate,
  STARTTIME           DATE,
  FINISHTIME          DATE,
  OPERATORNAME        VARCHAR2(50),
  COMMEND_ID          VARCHAR2(64),
  CONTRACTOR_CODE     VARCHAR2(64),
  BERTH_NUM           VARCHAR2(8),
  CONTAINERID         INTEGER,
  CTOSERRORMSG        VARCHAR2(500),
  CTOSERRORCODE       VARCHAR2(50),
  EXCEPSTARTIME       DATE,
  EXCEPFINISHTIME     DATE,
  EXCEPUSER           VARCHAR2(50),
  DEVICEOPTIMELINESID INTEGER default 0,
  LINECODE            VARCHAR2(18),
  DISPATCHEDTO        VARCHAR2(20),
  SERVICECODE         VARCHAR2(20),
  SHIPAGENT           VARCHAR2(10),
  SHIPOWNER           VARCHAR2(18),
  DISPATCHTIME        DATE,
  PLC_OPEN_TIME       DATE,
  PLC_CLOSE_TIME      DATE,
  IDENT_START_TIME    DATE,
  IDENT_END_TIME      DATE,
  CHECK_FLAG          NUMBER(1) default 0,
  DMG_CODE            VARCHAR2(50),
  CHECK_START_TIME    DATE,
  CHECK_END_TIME      DATE,
  CHECKEDBY           VARCHAR2(50)
)
partition by list (ISARCHIVED)
(
  partition PCUR values ('N')
    tablespace CCTCCTDB,
  partition PHIS values ('Y')
    tablespace CCTCCTDB,
  partition PARC values ('A')
    tablespace CCTCCTDB
);
comment on table OCR.T_OCR_CNT
  is '��װ����Ϣ��';
comment on column OCR.T_OCR_CNT.DOCK_ID
  is '��װ��ID';
comment on column OCR.T_OCR_CNT.ISARCHIVED
  is '�Ƿ�鵵,''N''��ǰ����/Y��ʱ��ʷ���� /A�鵵����';
comment on column OCR.T_OCR_CNT.CONTAINER_NO
  is '�ն�ʶ����ţ�δʶ�������Ϊ��δʶ��';
comment on column OCR.T_OCR_CNT.RCONTAINER_NO
  is '��¼�޸��Ժ�����,����ͳ��ʶ����';
comment on column OCR.T_OCR_CNT.CONTAINER_SHAPE
  is '���ͣ���Ӧ�ն�ʶ��ISO';
comment on column OCR.T_OCR_CNT.RCONTAINER_SHAPE
  is '�޸ĺ����ͣ�����ͳ��ʶ����';
comment on column OCR.T_OCR_CNT.CONTAINER_SIZE
  is '��ߴ磬����40��С��20';
comment on column OCR.T_OCR_CNT.CONTAINER_HEIGHT
  is '�߶�';
comment on column OCR.T_OCR_CNT.PLATE_NO
  is '�ϳ��ţ��ն�ʶ��';
comment on column OCR.T_OCR_CNT.RPALGE_NO
  is '�޸ĺ��Ƴ��ţ�����ͳ��ʶ����';
comment on column OCR.T_OCR_CNT.CTYPE
  is '�ж�ʶ��˫�䣬0Ϊ���䣬1Ϊ˫��';
comment on column OCR.T_OCR_CNT.CWEIGT
  is '�������ն�д��';
comment on column OCR.T_OCR_CNT.DOCK_STATUS
  is 'װж״̬��0Ϊװ����1Ϊж�����ն�д��';
comment on column OCR.T_OCR_CNT.CONTAINER_POS
  is '��λ������д���ֵ��˫���£�F��A������Ĭ��M��ͨ���ۿ���������������ն�ץ��ͼƬд�����ݿ⣬��������ͼƬʶ������Ҫ����㲻��';
comment on column OCR.T_OCR_CNT.CSTATUS
  is '���Ӵ���״̬��-4/��������ҵ -3/�ظ���¼ -2/��QC��ҵ���� -1/װ��δ�Զ�����, 0/׼���˹�����,1/���˹���ȡ,2/�ѳɹ����� 3/ת�쳣 4/�쳣�������';
comment on column OCR.T_OCR_CNT.CONFIDENCE
  is '���Ŷȣ��ն˴���';
comment on column OCR.T_OCR_CNT.TRVALCRANE_ID
  is '�ŵ�ID��ƽ̨���ú��ŵ���Ӧ�ն˺󣬶�Ӧ�ն˵����ݻ�����ŵ�ID�����ڱ�ʶ�������ĸ��ŵ�����';
comment on column OCR.T_OCR_CNT.TRVAL_NO
  is '�ŵ���';
comment on column OCR.T_OCR_CNT.DRIVER_NO
  is '��˾���ţ���������ӵ�˾����ţ���Ӧ�ŵ����ñ����������';
comment on column OCR.T_OCR_CNT.LANE_NO
  is '�����ţ��ն�ʶ��';
comment on column OCR.T_OCR_CNT.BEGIN_TIME
  is '��ʼ����ʱ�䣬�ն˴���';
comment on column OCR.T_OCR_CNT.END_TIME
  is '��������ʱ�䣬�ն�д��';
comment on column OCR.T_OCR_CNT.USER_ID
  is '�û�ID';
comment on column OCR.T_OCR_CNT.CNAM
  is '���Ĵ���';
comment on column OCR.T_OCR_CNT.ENAM
  is '������';
comment on column OCR.T_OCR_CNT.SHIP_CODE
  is '��������';
comment on column OCR.T_OCR_CNT.C_VOYAGE
  is '����';
comment on column OCR.T_OCR_CNT.PIC_NUM
  is '���Ӷ�ӦͼƬ����';
comment on column OCR.T_OCR_CNT.MSG_INDEX
  is '��Ϣid���ն��ϴ���˫��������������ͬ';
comment on column OCR.T_OCR_CNT.CARCONT
  is '��ͷ��������˫�����ж�ͼƬλ�ã���ptas����д�룬�м��㲻���Ŀ��ܡ�Ĭ��0,1������2������';
comment on column OCR.T_OCR_CNT.GANGS
  is '���������¼��';
comment on column OCR.T_OCR_CNT.CONTAINER_DIR
  is '���ŷ���';
comment on column OCR.T_OCR_CNT.STREAM_DIR
  is '����';
comment on column OCR.T_OCR_CNT.LOADING_PORT
  is 'װ����';
comment on column OCR.T_OCR_CNT.UNLOADING_PORT
  is 'ж����';
comment on column OCR.T_OCR_CNT.DEST_PORT
  is 'Ŀ�ĸ�';
comment on column OCR.T_OCR_CNT.BAY_HORIZONTAL
  is '����X';
comment on column OCR.T_OCR_CNT.BAY_VERTICAL
  is '����Y';
comment on column OCR.T_OCR_CNT.BAY
  is 'Bayλ';
comment on column OCR.T_OCR_CNT.CTIME
  is '����ʱ�� --OCR';
comment on column OCR.T_OCR_CNT.PMS_ID
  is 'ͼƬ������Id';
comment on column OCR.T_OCR_CNT.COPYTIME
  is 'ͬ������ʱ��';
comment on column OCR.T_OCR_CNT.STARTTIME
  is '����Ա��ȡͼƬʱ��';
comment on column OCR.T_OCR_CNT.FINISHTIME
  is '����Ա���ͼƬ����ʱ��';
comment on column OCR.T_OCR_CNT.OPERATORNAME
  is '����Ա';
comment on column OCR.T_OCR_CNT.COMMEND_ID
  is 'ָ���ֹ���';
comment on column OCR.T_OCR_CNT.CONTRACTOR_CODE
  is '�а��̴���';
comment on column OCR.T_OCR_CNT.BERTH_NUM
  is '��λ��';
comment on column OCR.T_OCR_CNT.CONTAINERID
  is '��ID';
comment on column OCR.T_OCR_CNT.CTOSERRORMSG
  is 'CTOS������Ϣ';
comment on column OCR.T_OCR_CNT.CTOSERRORCODE
  is 'CTOS�������';
comment on column OCR.T_OCR_CNT.EXCEPSTARTIME
  is '�쳣����ʼʱ��';
comment on column OCR.T_OCR_CNT.EXCEPFINISHTIME
  is '�쳣������ʱ��';
comment on column OCR.T_OCR_CNT.EXCEPUSER
  is '�쳣������';
comment on column OCR.T_OCR_CNT.DEVICEOPTIMELINESID
  is 'MOVE ID';
comment on column OCR.T_OCR_CNT.LINECODE
  is '����';
comment on column OCR.T_OCR_CNT.DISPATCHEDTO
  is '����˾����';
comment on column OCR.T_OCR_CNT.SERVICECODE
  is '���ߴ���';
comment on column OCR.T_OCR_CNT.SHIPAGENT
  is '����';
comment on column OCR.T_OCR_CNT.SHIPOWNER
  is '����';
comment on column OCR.T_OCR_CNT.DISPATCHTIME
  is '�ַ��������ʱ��';
comment on column OCR.T_OCR_CNT.PLC_OPEN_TIME
  is 'plc����ʱ��';
comment on column OCR.T_OCR_CNT.PLC_CLOSE_TIME
  is 'plc����ʱ��';
comment on column OCR.T_OCR_CNT.IDENT_START_TIME
  is 'ʶ��ʼʱ��';
comment on column OCR.T_OCR_CNT.IDENT_END_TIME
  is 'ʶ�����ʱ��';
comment on column OCR.T_OCR_CNT.CHECK_FLAG
  is 'ж�������־ 0δ��/1������/2����';
comment on column OCR.T_OCR_CNT.DMG_CODE
  is '�������';
comment on column OCR.T_OCR_CNT.CHECK_START_TIME
  is '���俪ʼʱ��';
comment on column OCR.T_OCR_CNT.CHECK_END_TIME
  is '�������ʱ��';
comment on column OCR.T_OCR_CNT.CHECKEDBY
  is '����Ա';
alter table OCR.T_OCR_CNT
  add constraint PK_T_OCR_CNT primary key (DOCK_ID);
create index OCR.IX_T_OCR_CNT on OCR.T_OCR_CNT (MSG_INDEX, TRVAL_NO);
create index OCR.IX_T_OCR_CNT2 on OCR.T_OCR_CNT (CTIME);
create index OCR.IX_T_OCR_CNT3 on OCR.T_OCR_CNT (CONTAINER_NO, RCONTAINER_NO);
create index OCR.IX_T_OCR_CNT4 on OCR.T_OCR_CNT (CONTAINERID);

prompt
prompt Creating table T_OCR_ERRORLOG
prompt =============================
prompt
create table OCR.T_OCR_ERRORLOG
(
  DATAID     INTEGER not null,
  QCNO       VARCHAR2(10),
  DEVICTTYPE VARCHAR2(20),
  DEVICENAME VARCHAR2(20),
  ERRORTYPE  VARCHAR2(50),
  STARTTIME  DATE,
  ENDTIME    DATE,
  ERRORMSG   VARCHAR2(50)
)
;
comment on table OCR.T_OCR_ERRORLOG
  is '������־';
comment on column OCR.T_OCR_ERRORLOG.QCNO
  is '�ź�';
comment on column OCR.T_OCR_ERRORLOG.DEVICTTYPE
  is '�豸����';
comment on column OCR.T_OCR_ERRORLOG.DEVICENAME
  is '�豸��';
comment on column OCR.T_OCR_ERRORLOG.ERRORTYPE
  is '��������';
comment on column OCR.T_OCR_ERRORLOG.STARTTIME
  is '��ʼʱ��';
comment on column OCR.T_OCR_ERRORLOG.ENDTIME
  is '����ʱ��';
comment on column OCR.T_OCR_ERRORLOG.ERRORMSG
  is '������Ϣ';
alter table OCR.T_OCR_ERRORLOG
  add constraint PK_T_OCR_ERROR_LOG primary key (DATAID);
create index OCR.IX_T_OCR_ERROR_LOG1 on OCR.T_OCR_ERRORLOG (QCNO, DEVICTTYPE);
create index OCR.IX_T_OCR_ERROR_LOG2 on OCR.T_OCR_ERRORLOG (STARTTIME, ENDTIME);

prompt
prompt Creating table T_OCR_EXP
prompt ========================
prompt
create table OCR.T_OCR_EXP
(
  SEQNO      INTEGER not null,
  APPNAME    VARCHAR2(50) not null,
  SYMPTOM    VARCHAR2(300) not null,
  SOLUTION   VARCHAR2(1000) not null,
  INPUTEDBY  VARCHAR2(50) not null,
  INPUTETIME DATE default sysdate not null
)
;
comment on table OCR.T_OCR_EXP
  is '��ά֪ʶ��';
comment on column OCR.T_OCR_EXP.SEQNO
  is '���';
comment on column OCR.T_OCR_EXP.APPNAME
  is 'Ӧ������';
comment on column OCR.T_OCR_EXP.SYMPTOM
  is '��������';
comment on column OCR.T_OCR_EXP.SOLUTION
  is '����취';
comment on column OCR.T_OCR_EXP.INPUTEDBY
  is '¼����';
comment on column OCR.T_OCR_EXP.INPUTETIME
  is '¼��ʱ��';
alter table OCR.T_OCR_EXP
  add constraint PK_T_OCR_EXP primary key (SEQNO);
create index SYS.IX_T_OCR_EXP1 on OCR.T_OCR_EXP (APPNAME);

prompt
prompt Creating table T_OCR_LOG
prompt ========================
prompt
create table OCR.T_OCR_LOG
(
  LOGID      INTEGER not null,
  TABLENAME  VARCHAR2(50),
  COLNAME    VARCHAR2(50),
  OLDVALUE   VARCHAR2(1000),
  NEWVALUE   VARCHAR2(1000),
  UPDATEDBY  VARCHAR2(50),
  UPDATETIME DATE
)
;
comment on table OCR.T_OCR_LOG
  is '������־';
comment on column OCR.T_OCR_LOG.LOGID
  is '��־ID';
comment on column OCR.T_OCR_LOG.TABLENAME
  is '���±�';
comment on column OCR.T_OCR_LOG.COLNAME
  is '��������ؼ�ֵ��';
comment on column OCR.T_OCR_LOG.OLDVALUE
  is '��ֵ';
comment on column OCR.T_OCR_LOG.NEWVALUE
  is '��ֵ';
comment on column OCR.T_OCR_LOG.UPDATEDBY
  is '������';
comment on column OCR.T_OCR_LOG.UPDATETIME
  is '����ʱ��';
alter table OCR.T_OCR_LOG
  add constraint PK_T_OCR_LOG primary key (LOGID);
create index OCR.IX_T_OCR_LOG1 on OCR.T_OCR_LOG (TABLENAME, COLNAME);

prompt
prompt Creating table T_OCR_MAC
prompt ========================
prompt
create table OCR.T_OCR_MAC
(
  QCNO    VARCHAR2(10) not null,
  MACADDR VARCHAR2(100)
)
;
comment on table OCR.T_OCR_MAC
  is 'MAC��ַ��';
alter table OCR.T_OCR_MAC
  add constraint PK_T_OCR_MAC primary key (QCNO);

prompt
prompt Creating table T_OCR_MONITORLOG
prompt ===============================
prompt
create table OCR.T_OCR_MONITORLOG
(
  SEQNO      INTEGER not null,
  HANLDETIME DATE,
  HANDLEFROM VARCHAR2(50),
  HANDLECMD  VARCHAR2(100),
  HANDLETO   VARCHAR2(200)
)
;
comment on table OCR.T_OCR_MONITORLOG
  is '��ز�����־';
comment on column OCR.T_OCR_MONITORLOG.HANLDETIME
  is '����ʱ��';
comment on column OCR.T_OCR_MONITORLOG.HANDLEFROM
  is '��������';
comment on column OCR.T_OCR_MONITORLOG.HANDLECMD
  is '��������';
comment on column OCR.T_OCR_MONITORLOG.HANDLETO
  is 'Ŀ�����';
alter table OCR.T_OCR_MONITORLOG
  add constraint PK_T_OCR_MONITORLOG primary key (SEQNO);
create index OCR.IX_T_OCR_MONITORLOG1 on OCR.T_OCR_MONITORLOG (HANLDETIME);

prompt
prompt Creating table T_OCR_PARAMS
prompt ===========================
prompt
create table OCR.T_OCR_PARAMS
(
  PARAMNAME  VARCHAR2(100) not null,
  PARAMVALUE VARCHAR2(1000)
)
;
comment on table OCR.T_OCR_PARAMS
  is 'ϵͳ����';
comment on column OCR.T_OCR_PARAMS.PARAMNAME
  is '������';
comment on column OCR.T_OCR_PARAMS.PARAMVALUE
  is '����ֵ';
alter table OCR.T_OCR_PARAMS
  add constraint PK_T_OCR_PARAMS primary key (PARAMNAME);

prompt
prompt Creating table T_OCR_PHOTO
prompt ==========================
prompt
create table OCR.T_OCR_PHOTO
(
  PHOTO_ID                NUMBER(20) not null,
  ISARCHIVED              VARCHAR2(1) default 'N',
  DOCK_ID                 NUMBER(20),
  PHOTO_URL               VARCHAR2(512),
  CTIME                   DATE,
  PHOTO_NAME              VARCHAR2(64),
  PHOTO_POS               NUMBER(2),
  CONT_ORDER              NUMBER(2),
  COPYTIME                DATE default SYSDATE,
  DATA_DELAY              NUMBER(8),
  PLCDATA_BOXHEIGHT       NUMBER(8),
  PLCDATA_BOXDISPLACEMENT NUMBER(8)
)
partition by list (ISARCHIVED)
(
  partition PCUR values ('N')
    tablespace CCTCCTDB,
  partition PHIS values ('Y')
    tablespace CCTCCTDB
);
comment on table OCR.T_OCR_PHOTO
  is 'ͼƬ';
comment on column OCR.T_OCR_PHOTO.PHOTO_ID
  is 'id�����У�';
comment on column OCR.T_OCR_PHOTO.ISARCHIVED
  is '�Ƿ�鵵';
comment on column OCR.T_OCR_PHOTO.DOCK_ID
  is '����id��4������id';
comment on column OCR.T_OCR_PHOTO.PHOTO_URL
  is 'ͼƬurl';
comment on column OCR.T_OCR_PHOTO.CTIME
  is '����ʱ�� OCR';
comment on column OCR.T_OCR_PHOTO.PHOTO_NAME
  is 'ͼƬ����';
comment on column OCR.T_OCR_PHOTO.PHOTO_POS
  is 'ͼƬλ��';
comment on column OCR.T_OCR_PHOTO.CONT_ORDER
  is '����˳��';
comment on column OCR.T_OCR_PHOTO.COPYTIME
  is 'ͬ������ʱ��';
comment on column OCR.T_OCR_PHOTO.DATA_DELAY
  is 'plc�ӳ�';
comment on column OCR.T_OCR_PHOTO.PLCDATA_BOXHEIGHT
  is '���ո߶�';
comment on column OCR.T_OCR_PHOTO.PLCDATA_BOXDISPLACEMENT
  is '���պ�����';
alter table OCR.T_OCR_PHOTO
  add constraint PK_T_OCR_PHOTO primary key (PHOTO_ID);
create index OCR.IX_T_OCR_PHOTO1 on OCR.T_OCR_PHOTO (DOCK_ID);

prompt
prompt Creating table T_OCR_QCSET
prompt ==========================
prompt
create table OCR.T_OCR_QCSET
(
  WORKSET_ID        NUMBER(8),
  TRVALCRANE_ID     NUMBER(8),
  TRVALCRANE_NO     VARCHAR2(64) not null,
  CONTRACTOR_CODE   VARCHAR2(64),
  COMMEND_ID        VARCHAR2(64),
  DRIVER_ID         VARCHAR2(64),
  SHIP_CODE         VARCHAR2(64),
  IN_VOYAGE_CODE    VARCHAR2(64),
  OUT_VOYAGE_CODE   VARCHAR2(64),
  BERTH_NUM         VARCHAR2(8),
  VOYAGE_TYPE       VARCHAR2(2),
  SHIPMENT_DEAL     VARCHAR2(10),
  OPERATOR_UID      VARCHAR2(50),
  COMMEND_PAW       VARCHAR2(64),
  TERMINAL_NO       VARCHAR2(64),
  TICKET_ID         VARCHAR2(600),
  DEVICE_NO         VARCHAR2(64),
  DEVICE_TYPE       NUMBER(2),
  IS_CHANGE         NUMBER(2) default 0,
  STATUS            VARCHAR2(20),
  ERROR_MESS        VARCHAR2(512),
  CREATE_TIME       DATE default sysdate,
  UPDATE_TIME       DATE,
  BERTH_WAY         VARCHAR2(1),
  VESSELALIASE      VARCHAR2(50),
  BERTHPLANNO       NUMBER(38),
  AVESSELNAME       VARCHAR2(100),
  INAGENT           VARCHAR2(20),
  OUTAGENT          VARCHAR2(20),
  OWNER             VARCHAR2(18),
  INVESSELLINECODE  VARCHAR2(20),
  OUTVESSELLINECODE VARCHAR2(20),
  WORKTYPE          VARCHAR2(10) default '����',
  CAMERACODE        VARCHAR2(50)
)
;
comment on table OCR.T_OCR_QCSET
  is '�ŵ���ҵ����';
comment on column OCR.T_OCR_QCSET.WORKSET_ID
  is '�ŵ���ҵ����id';
comment on column OCR.T_OCR_QCSET.TRVALCRANE_ID
  is '�ŵ�id��1��trvalcrane_id';
comment on column OCR.T_OCR_QCSET.TRVALCRANE_NO
  is '�ŵ�no��1��trvalcrane_no';
comment on column OCR.T_OCR_QCSET.CONTRACTOR_CODE
  is '�а��̴���';
comment on column OCR.T_OCR_QCSET.COMMEND_ID
  is 'ָ���ֹ���';
comment on column OCR.T_OCR_QCSET.DRIVER_ID
  is 'QC˾������';
comment on column OCR.T_OCR_QCSET.SHIP_CODE
  is '��������';
comment on column OCR.T_OCR_QCSET.IN_VOYAGE_CODE
  is '���ں���';
comment on column OCR.T_OCR_QCSET.OUT_VOYAGE_CODE
  is '���ں���';
comment on column OCR.T_OCR_QCSET.BERTH_NUM
  is '��λ��';
comment on column OCR.T_OCR_QCSET.VOYAGE_TYPE
  is '���������ͣ�IM��ʾ���ڣ�EX��ʾ����';
comment on column OCR.T_OCR_QCSET.SHIPMENT_DEAL
  is 'װ���˹������أ� ��/��';
comment on column OCR.T_OCR_QCSET.OPERATOR_UID
  is '����Աid';
comment on column OCR.T_OCR_QCSET.COMMEND_PAW
  is 'ָ���ֵ�½����';
comment on column OCR.T_OCR_QCSET.TERMINAL_NO
  is '�豸�ն˺�';
comment on column OCR.T_OCR_QCSET.TICKET_ID
  is 'ָ���ֵ�½�ɹ��󷵻صĻỰ���';
comment on column OCR.T_OCR_QCSET.DEVICE_NO
  is 'ָ���ֵ�½�ɹ��󷵻ص��豸��';
comment on column OCR.T_OCR_QCSET.DEVICE_TYPE
  is 'ָ���ֵ�½�ɹ��󷵻ص��豸����';
comment on column OCR.T_OCR_QCSET.IS_CHANGE
  is '���ֶ���Ҫ��Ϊ�˿��ƿؼ����³�ʼ����ֻ���ŵ���ҵ�����޸Ĺ��ˣ������³�ʼ���ؼ�';
comment on column OCR.T_OCR_QCSET.STATUS
  is '�ŵ���ҵ����״̬��ֹͣ��ҵ����ͣ��ҵ����ҵ��';
comment on column OCR.T_OCR_QCSET.ERROR_MESS
  is 'Ctos���ص���Ϣ';
comment on column OCR.T_OCR_QCSET.CREATE_TIME
  is '����ʱ��';
comment on column OCR.T_OCR_QCSET.UPDATE_TIME
  is '����ʱ��';
comment on column OCR.T_OCR_QCSET.BERTH_WAY
  is '�������� R/L';
comment on column OCR.T_OCR_QCSET.VESSELALIASE
  is '������';
comment on column OCR.T_OCR_QCSET.BERTHPLANNO
  is '����ID';
comment on column OCR.T_OCR_QCSET.AVESSELNAME
  is '��Ӣ����';
comment on column OCR.T_OCR_QCSET.INAGENT
  is '���ڴ���';
comment on column OCR.T_OCR_QCSET.OUTAGENT
  is '���ڴ���';
comment on column OCR.T_OCR_QCSET.OWNER
  is '����';
comment on column OCR.T_OCR_QCSET.INVESSELLINECODE
  is '���ں���';
comment on column OCR.T_OCR_QCSET.OUTVESSELLINECODE
  is '���ں���';
comment on column OCR.T_OCR_QCSET.WORKTYPE
  is '��ҵģʽ����������';
comment on column OCR.T_OCR_QCSET.CAMERACODE
  is 'ȫ����ͷ����';
alter table OCR.T_OCR_QCSET
  add constraint PK_T_OCR_QCSET primary key (TRVALCRANE_NO);

prompt
prompt Creating table T_OCR_STATIC_QC
prompt ==============================
prompt
create table OCR.T_OCR_STATIC_QC
(
  WORKPOINTNO    VARCHAR2(30) not null,
  LOADCOMPLETED  NUMBER,
  DISCOMPLETED   NUMBER,
  LOADLEFT       NUMBER,
  DISLEFT        NUMBER,
  LASTUPDATETIME DATE
)
;
comment on table OCR.T_OCR_STATIC_QC
  is '�ָ�ͳ��';
comment on column OCR.T_OCR_STATIC_QC.WORKPOINTNO
  is '������';
comment on column OCR.T_OCR_STATIC_QC.LOADCOMPLETED
  is 'װ�����';
comment on column OCR.T_OCR_STATIC_QC.DISCOMPLETED
  is 'ж�����';
comment on column OCR.T_OCR_STATIC_QC.LOADLEFT
  is 'װ��ʣ��';
comment on column OCR.T_OCR_STATIC_QC.DISLEFT
  is 'ж��ʣ��';
alter table OCR.T_OCR_STATIC_QC
  add constraint PK_T_OCR_STATIC_QC primary key (WORKPOINTNO);

prompt
prompt Creating table T_OCR_STATIC_VS
prompt ==============================
prompt
create table OCR.T_OCR_STATIC_VS
(
  BERTHPLANNO    NUMBER(38) not null,
  CVESSELNAME    VARCHAR2(160),
  LOADCOUNT      NUMBER default 0,
  DISCHARGECOUNT NUMBER default 0,
  TTL            NUMBER default 0,
  LOADLEFT       NUMBER default 0,
  DISLEFT        NUMBER default 0,
  BERTHNO        VARCHAR2(10),
  TERMINALCODE   VARCHAR2(10),
  VESSELTYPE     VARCHAR2(2),
  LASTUPDATETIME DATE
)
;
comment on table OCR.T_OCR_STATIC_VS
  is '������ҵͳ�Ʊ�';
comment on column OCR.T_OCR_STATIC_VS.BERTHPLANNO
  is '����ID';
comment on column OCR.T_OCR_STATIC_VS.CVESSELNAME
  is '���Ĵ���';
comment on column OCR.T_OCR_STATIC_VS.LOADCOUNT
  is 'װ��';
comment on column OCR.T_OCR_STATIC_VS.DISCHARGECOUNT
  is 'ж��';
comment on column OCR.T_OCR_STATIC_VS.TTL
  is '��װж��';
comment on column OCR.T_OCR_STATIC_VS.LOADLEFT
  is 'װ������';
comment on column OCR.T_OCR_STATIC_VS.DISLEFT
  is 'ж������';
comment on column OCR.T_OCR_STATIC_VS.BERTHNO
  is '��λ';
comment on column OCR.T_OCR_STATIC_VS.TERMINALCODE
  is '����';
alter table OCR.T_OCR_STATIC_VS
  add constraint PK_T_OCR_STATIC_VS primary key (BERTHPLANNO);

prompt
prompt Creating table T_OCR_TRUCK
prompt ==========================
prompt
create table OCR.T_OCR_TRUCK
(
  SEQNO      INTEGER not null,
  QCNO       VARCHAR2(8),
  TRUCKNO    VARCHAR2(8),
  CREATETIME DATE,
  COPYTIME   DATE,
  ISARCHIVED VARCHAR2(1) default 'N' not null
)
partition by list (ISARCHIVED)
(
  partition PCUR values ('N')
    tablespace CCTOCR,
  partition PHIS values ('Y')
    tablespace CCTOCR
);
comment on table OCR.T_OCR_TRUCK
  is '������ǰץ��';
comment on column OCR.T_OCR_TRUCK.QCNO
  is '���ź�';
comment on column OCR.T_OCR_TRUCK.TRUCKNO
  is '�ϳ���';
comment on column OCR.T_OCR_TRUCK.CREATETIME
  is '����ʱ��';
comment on column OCR.T_OCR_TRUCK.COPYTIME
  is 'ͬ��ʱ��';
comment on column OCR.T_OCR_TRUCK.ISARCHIVED
  is '�Ƿ�鵵';
alter table OCR.T_OCR_TRUCK
  add constraint T_OCR_TRUCK primary key (SEQNO);
create index OCR.IX_OCR_TRUCK1 on OCR.T_OCR_TRUCK (QCNO);
create index OCR.IX_OCR_TRUCK2 on OCR.T_OCR_TRUCK (CREATETIME);

prompt
prompt Creating sequence SEQ_EXP
prompt =========================
prompt
create sequence OCR.SEQ_EXP
minvalue 1
maxvalue 999999999999999
start with 11
increment by 1
nocache;

prompt
prompt Creating sequence SEQ_OCR
prompt =========================
prompt
create sequence OCR.SEQ_OCR
minvalue 1
maxvalue 9999999999999
start with 148357
increment by 1
cache 20;

prompt
prompt Creating sequence SEQ_OCRX
prompt ==========================
prompt
create sequence OCR.SEQ_OCRX
minvalue 1
maxvalue 9999999999999999
start with 1
increment by 1
cache 20;

prompt
prompt Creating sequence SEQ_OCRX_CNT
prompt ==============================
prompt
create sequence OCR.SEQ_OCRX_CNT
minvalue 1
maxvalue 9999999999999999
start with 6621
increment by 1
cache 20;

prompt
prompt Creating sequence SEQ_OCRX_PHOTO
prompt ================================
prompt
create sequence OCR.SEQ_OCRX_PHOTO
minvalue 1
maxvalue 9999999999999999999
start with 8241
increment by 1
cache 20;

prompt
prompt Creating package PKG_OCR
prompt ========================
prompt
CREATE OR REPLACE PACKAGE OCR.pkg_ocr IS

  -- Author  : YEJT
  -- Created : 2016/4/5 10:34:55
  -- Purpose : 

  -- Public type declarations
  TYPE mycursor IS REF CURSOR;

  --�鵱ǰ���ID
  PROCEDURE p_select_maxid(ret_value OUT mycursor);

  --�鵱ǰ���ID ͼƬ
  PROCEDURE p_select_maxidphoto(ret_value OUT mycursor);

  --�鵱ǰ���ID ����
  PROCEDURE p_select_maxidtruck(ret_value OUT mycursor);

  --������ʶ���¼
  PROCEDURE p_insert_cnt(p_dock_id          NUMBER,
                         p_container_no     VARCHAR2,
                         p_container_shape  VARCHAR2,
                         p_container_size   NUMBER,
                         p_container_height NUMBER,
                         p_plate_no         VARCHAR2,
                         p_ctype            NUMBER,
                         p_cweigt           NUMBER,
                         p_dock_status      NUMBER,
                         p_container_pos    VARCHAR2,
                         p_cstatus          NUMBER,
                         p_confidence       NUMBER,
                         p_trvalcrane_id    NUMBER,
                         p_trval_no         VARCHAR2,
                         p_driver_no        VARCHAR2,
                         p_lane_no          NUMBER,
                         p_begin_time       DATE,
                         p_end_time         DATE,
                         p_user_id          VARCHAR2,
                         p_cnam             VARCHAR2,
                         p_enam             VARCHAR2,
                         p_ship_code        VARCHAR2,
                         p_c_voyage         VARCHAR2,
                         p_pic_num          NUMBER,
                         p_msg_index        NUMBER,
                         p_carcont          NUMBER,
                         p_gangs            VARCHAR2,
                         p_container_dir    VARCHAR2,
                         p_stream_dir       VARCHAR2,
                         p_loading_port     VARCHAR2,
                         p_unloading_port   VARCHAR2,
                         p_dest_port        VARCHAR2,
                         p_bay_horizontal   VARCHAR2,
                         p_bay_vertical     VARCHAR2,
                         p_bay              VARCHAR2,
                         p_ctime            DATE,
                         p_pms_id           NUMBER,
                         p_commend_id       VARCHAR2,
                         p_berth_num        VARCHAR2,
                         p_isarchived       VARCHAR2);

  --������ʶ���¼
  PROCEDURE p_insert_cnt2(p_dock_id          NUMBER,
                          p_container_no     VARCHAR2,
                          p_container_shape  VARCHAR2,
                          p_container_size   NUMBER,
                          p_container_height NUMBER,
                          p_plate_no         VARCHAR2,
                          p_ctype            NUMBER,
                          p_cweigt           NUMBER,
                          p_dock_status      NUMBER,
                          p_container_pos    VARCHAR2,
                          p_cstatus          NUMBER,
                          p_confidence       NUMBER,
                          p_trvalcrane_id    NUMBER,
                          p_trval_no         VARCHAR2,
                          p_driver_no        VARCHAR2,
                          p_lane_no          NUMBER,
                          p_begin_time       DATE,
                          p_end_time         DATE,
                          p_user_id          VARCHAR2,
                          p_cnam             VARCHAR2,
                          p_enam             VARCHAR2,
                          p_ship_code        VARCHAR2,
                          p_c_voyage         VARCHAR2,
                          p_pic_num          NUMBER,
                          p_msg_index        NUMBER,
                          p_carcont          NUMBER,
                          p_gangs            VARCHAR2,
                          p_container_dir    VARCHAR2,
                          p_stream_dir       VARCHAR2,
                          p_loading_port     VARCHAR2,
                          p_unloading_port   VARCHAR2,
                          p_dest_port        VARCHAR2,
                          p_bay_horizontal   VARCHAR2,
                          p_bay_vertical     VARCHAR2,
                          p_bay              VARCHAR2,
                          p_ctime            DATE,
                          p_pms_id           NUMBER,
                          p_commend_id       VARCHAR2,
                          p_berth_num        VARCHAR2,
                          p_isarchived       VARCHAR2,
                          p_servicecode      VARCHAR2,
                          p_shipagent        VARCHAR2,
                          p_shipowner        VARCHAR2);
  --������ʶ���¼
  PROCEDURE p_insert_cnt3(p_dock_id          NUMBER,
                          p_container_no     VARCHAR2,
                          p_container_shape  VARCHAR2,
                          p_container_size   NUMBER,
                          p_container_height NUMBER,
                          p_plate_no         VARCHAR2,
                          p_ctype            NUMBER,
                          p_cweigt           NUMBER,
                          p_dock_status      NUMBER,
                          p_container_pos    VARCHAR2,
                          p_cstatus          NUMBER,
                          p_confidence       NUMBER,
                          p_trvalcrane_id    NUMBER,
                          p_trval_no         VARCHAR2,
                          p_driver_no        VARCHAR2,
                          p_lane_no          NUMBER,
                          p_begin_time       DATE,
                          p_end_time         DATE,
                          p_user_id          VARCHAR2,
                          p_cnam             VARCHAR2,
                          p_enam             VARCHAR2,
                          p_ship_code        VARCHAR2,
                          p_c_voyage         VARCHAR2,
                          p_pic_num          NUMBER,
                          p_msg_index        NUMBER,
                          p_carcont          NUMBER,
                          p_gangs            VARCHAR2,
                          p_container_dir    VARCHAR2,
                          p_stream_dir       VARCHAR2,
                          p_loading_port     VARCHAR2,
                          p_unloading_port   VARCHAR2,
                          p_dest_port        VARCHAR2,
                          p_bay_horizontal   VARCHAR2,
                          p_bay_vertical     VARCHAR2,
                          p_bay              VARCHAR2,
                          p_ctime            DATE,
                          p_pms_id           NUMBER,
                          p_commend_id       VARCHAR2,
                          p_berth_num        VARCHAR2,
                          p_isarchived       VARCHAR2,
                          p_servicecode      VARCHAR2,
                          p_shipagent        VARCHAR2,
                          p_shipowner        VARCHAR2,
                          p_plc_open_time    DATE,
                          p_plc_close_time   DATE,
                          p_ident_start_time DATE,
                          p_ident_end_time   DATE);
  --����ͼƬ��¼
  PROCEDURE p_insert_photo(p_photo_id   NUMBER,
                           p_dock_id    NUMBER,
                           p_photo_url  VARCHAR2,
                           p_ctime      DATE,
                           p_photo_name VARCHAR2,
                           p_photo_pos  VARCHAR2,
                           p_cont_order NUMBER);

  --����ͼƬ��¼
  PROCEDURE p_insert_photo2(p_photo_id                NUMBER,
                            p_dock_id                 NUMBER,
                            p_photo_url               VARCHAR2,
                            p_ctime                   DATE,
                            p_photo_name              VARCHAR2,
                            p_photo_pos               VARCHAR2,
                            p_cont_order              NUMBER,
                            p_data_delay              NUMBER,
                            p_plcdata_boxheight       NUMBER,
                            p_plcdata_boxdisplacement NUMBER);

  --���복�ż�¼
  PROCEDURE p_insert_truck(p_seqno      NUMBER,
                           p_qcno       VARCHAR2,
                           p_truckno    VARCHAR2,
                           p_createtime DATE);

  --���ŵ���ҵ����
  PROCEDURE p_select_qcset(ret_value OUT mycursor);

  --���ŵ���ҵ����
  PROCEDURE p_select_qcset1(ret_value OUT mycursor, p_qc VARCHAR2);

  --�����ŵ���ҵ����
  PROCEDURE p_update_qcset(p_trvalcrane_no   VARCHAR2,
                           p_contractor_code VARCHAR2,
                           p_commend_id      VARCHAR2,
                           p_driver_id       VARCHAR2,
                           p_ship_code       VARCHAR2,
                           p_in_voyage_code  VARCHAR2,
                           p_out_voyage_code VARCHAR2,
                           p_berth_num       VARCHAR2,
                           p_voyage_type     VARCHAR2,
                           p_shipment_deal   VARCHAR2,
                           p_operator_uid    VARCHAR2,
                           p_commend_paw     VARCHAR2,
                           p_terminal_no     VARCHAR2,
                           p_ticket_id       VARCHAR2,
                           p_device_no       VARCHAR2,
                           p_device_type     NUMBER,
                           p_is_change       NUMBER,
                           p_status          VARCHAR2,
                           p_error_mess      VARCHAR2,
                           p_berth_way       VARCHAR2,
                           p_vesselaliase    VARCHAR2,
                           p_berthplanno     NUMBER,
                           p_avesselname     VARCHAR2);

  --�����ŵ���ҵ����
  PROCEDURE p_update_qcset2(p_trvalcrane_no     VARCHAR2,
                            p_contractor_code   VARCHAR2,
                            p_commend_id        VARCHAR2,
                            p_driver_id         VARCHAR2,
                            p_ship_code         VARCHAR2,
                            p_in_voyage_code    VARCHAR2,
                            p_out_voyage_code   VARCHAR2,
                            p_berth_num         VARCHAR2,
                            p_voyage_type       VARCHAR2,
                            p_shipment_deal     VARCHAR2,
                            p_operator_uid      VARCHAR2,
                            p_commend_paw       VARCHAR2,
                            p_terminal_no       VARCHAR2,
                            p_ticket_id         VARCHAR2,
                            p_device_no         VARCHAR2,
                            p_device_type       NUMBER,
                            p_is_change         NUMBER,
                            p_status            VARCHAR2,
                            p_error_mess        VARCHAR2,
                            p_berth_way         VARCHAR2,
                            p_vesselaliase      VARCHAR2,
                            p_berthplanno       NUMBER,
                            p_avesselname       VARCHAR2,
                            p_inagent           VARCHAR2,
                            p_outagent          VARCHAR2,
                            p_owner             VARCHAR2,
                            p_invessellinecode  VARCHAR2,
                            p_outvessellinecode VARCHAR2,
                            p_worktype          VARCHAR2);

  --����һ��ʶ����
  PROCEDURE p_select_nextrecord(ret_value  OUT mycursor,
                                p_username VARCHAR2);

  --����һ��ʶ���� �ָ���
  PROCEDURE p_select_nextrecord2(ret_value  OUT mycursor,
                                 p_username VARCHAR2,
                                 p_terminal VARCHAR2);

  --���Ӧ����Ƭ��¼
  PROCEDURE p_select_photo(ret_value OUT mycursor, p_dock_id NUMBER);

  --��ȡϵͳ����
  PROCEDURE p_select_params(ret_value OUT mycursor);

  --����ʶ���¼������
  PROCEDURE p_update_cntstatus(p_dock_id          NUMBER,
                               p_cstatus          NUMBER,
                               p_operatorname     VARCHAR2,
                               p_containerid      NUMBER,
                               p_ctoserrormsg     VARCHAR2,
                               p_ctoserrorcode    VARCHAR2,
                               p_isarchived       VARCHAR2,
                               p_rcontainer_no    VARCHAR2,
                               p_rcontainer_shape VARCHAR2,
                               p_rpalge_no        VARCHAR2);
  --����ʶ���¼������
  PROCEDURE p_update_cntstatus2(p_dock_id          NUMBER,
                                p_cstatus          NUMBER,
                                p_operatorname     VARCHAR2,
                                p_containerid      NUMBER,
                                p_ctoserrormsg     VARCHAR2,
                                p_ctoserrorcode    VARCHAR2,
                                p_isarchived       VARCHAR2,
                                p_rcontainer_no    VARCHAR2,
                                p_rcontainer_shape VARCHAR2,
                                p_rpalge_no        VARCHAR2,
                                p_dock_status      NUMBER,
                                p_linecode         VARCHAR2,
                                p_container_no     VARCHAR2);

  --����ʶ���¼������
  PROCEDURE p_update_cntstatus3(p_dock_id          NUMBER,
                                p_cstatus          NUMBER,
                                p_operatorname     VARCHAR2,
                                p_containerid      NUMBER,
                                p_ctoserrormsg     VARCHAR2,
                                p_ctoserrorcode    VARCHAR2,
                                p_isarchived       VARCHAR2,
                                p_rcontainer_no    VARCHAR2,
                                p_rcontainer_shape VARCHAR2,
                                p_rpalge_no        VARCHAR2,
                                p_dock_status      NUMBER,
                                p_linecode         VARCHAR2,
                                p_container_no     VARCHAR2,
                                p_container_pos    VARCHAR2);

  --����һ��ʶ���� �ָ��� ������������
  PROCEDURE p_select_nextrecord3(ret_value  OUT mycursor,
                                 p_username VARCHAR2,
                                 p_terminal VARCHAR2);

  --����ʶ���¼���䴦����
  PROCEDURE p_update_cntcheck(p_dock_id    NUMBER,
                              p_isarchived VARCHAR2,
                              p_check_flag NUMBER,
                              p_dmg_code   VARCHAR2,
                              p_checkedby  VARCHAR2);

  --�����Զ�����װ���ļ�¼
  PROCEDURE p_select_loadauto(ret_value OUT mycursor);

  --�����Զ�����װ���ļ�¼
  PROCEDURE p_select_loadauto1(ret_value OUT mycursor, p_qc VARCHAR2);

  --�鿴������־
  PROCEDURE p_select_log(ret_value   OUT mycursor,
                         p_tablename VARCHAR2,
                         p_colname   VARCHAR2);

  --�鿴qc���
  PROCEDURE p_select_qcmonitor(ret_value OUT mycursor);

  --�鿴�ϳ����
  PROCEDURE p_select_truckmonitor(ret_value OUT mycursor);

  --�鵵����
  PROCEDURE p_archive;

  --������δ������쳣
  PROCEDURE p_select_excep(ret_value OUT mycursor);

  --�����쳣������
  PROCEDURE p_update_excepstatus(p_dock_id    NUMBER,
                                 p_cstatus    NUMBER,
                                 p_excepuser  VARCHAR2,
                                 p_isarchived VARCHAR2);

  --�����쳣������
  PROCEDURE p_update_excepstatus2(p_dock_id       NUMBER,
                                  p_cstatus       NUMBER,
                                  p_excepuser     VARCHAR2,
                                  p_isarchived    VARCHAR2,
                                  p_rcontainer_no VARCHAR2,
                                  p_linecode      VARCHAR2,
                                  p_containerid   NUMBER);

  --��ʣ���¼��
  PROCEDURE p_select_left(ret_value OUT mycursor);

  --��ʣ���¼�� �ָ���
  PROCEDURE p_select_left2(ret_value OUT mycursor, p_terminal VARCHAR2);

  --���ּ��
  PROCEDURE p_select_vslmonitor(ret_cursor OUT mycursor);

  --����Ϣ���
  PROCEDURE p_select_cntmonitor(ret_cursor OUT mycursor);

  --װ�����
  PROCEDURE p_select_loadmonitor(ret_cursor OUT mycursor);

  --��װ������
  PROCEDURE p_select_loaded(ret_cursor OUT mycursor,
                            p_from     DATE,
                            p_to       DATE);

  --ת�쳣����
  PROCEDURE p_select_excepmonitor(ret_cursor OUT mycursor,
                                  p_from     DATE,
                                  p_to       DATE);

  --��ѯ
  PROCEDURE p_select_cnt(ret_cursor    OUT mycursor,
                         p_from        DATE,
                         p_to          DATE,
                         p_isarchived  VARCHAR2,
                         p_qcno        VARCHAR2,
                         p_dock_status INTEGER,
                         p_cstatus     INTEGER,
                         p_containerno VARCHAR2);

  --��ѯ
  PROCEDURE p_select_cnt2(ret_cursor    OUT mycursor,
                          p_from        DATE,
                          p_to          DATE,
                          p_isarchived  VARCHAR2,
                          p_qcno        VARCHAR2,
                          p_dock_status INTEGER,
                          p_cstatus     INTEGER,
                          p_containerno VARCHAR2,
                          p_truckno     VARCHAR2);

  --���Ӧ����Ƭ��¼
  PROCEDURE p_select_photo2(ret_value OUT mycursor, p_dock_id NUMBER);

  --���ּ��
  PROCEDURE p_select_vslmonitor2(ret_cursor OUT mycursor);

  --����5����ҵָ�� 
  PROCEDURE p_select_lastfive(ret_value OUT mycursor, p_qc VARCHAR2);

  --�����ŵ����
  PROCEDURE p_select_vslmonitor3(ret_cursor OUT mycursor);

  --����ʶ���¼move id
  PROCEDURE p_update_moveid(p_msg_index           NUMBER,
                            p_trval_no            VARCHAR2,
                            p_deviceoptimelinesid NUMBER);

  --��ſ��
  PROCEDURE p_select_cntquery(ret_cursor    OUT mycursor,
                              p_containerno VARCHAR2);

  PROCEDURE p_select_vslmonitor31(ret_cursor OUT mycursor);
  PROCEDURE p_select_vslmonitor21(ret_cursor OUT mycursor);

  --���źŲ���
  PROCEDURE p_select_cntbyqcno(ret_cursor OUT mycursor, p_qcno VARCHAR2);

  --���Ӧ����Ƭ��¼
  PROCEDURE p_select_photo3(ret_value OUT mycursor, p_photo_id NUMBER);

  --��qc�ϵ��豸
  /*
  PROCEDURE p_select_qcdevice(ret_value OUT mycursor, p_qcno VARCHAR2);
  */

  PROCEDURE p_select_qcrate(ret_value OUT mycursor,
                            p_qcno    VARCHAR2,
                            p_type    NUMBER DEFAULT 1000);

  --���źŲ���
  PROCEDURE p_select_cntbyqcno2(ret_cursor OUT mycursor, p_qcno VARCHAR2);

  --���������־
  PROCEDURE p_save_errorlog(p_qcno       VARCHAR2,
                            p_devicttype VARCHAR2,
                            p_devicename VARCHAR2,
                            p_errortype  VARCHAR2,
                            p_starttime  DATE,
                            p_errormsg   VARCHAR2,
                            p_done       VARCHAR2);

  --�������־
  PROCEDURE p_select_errorlog(ret_value    OUT mycursor,
                              p_qcno       VARCHAR2,
                              p_devicetype VARCHAR2,
                              p_fromtime   DATE,
                              p_endtime    DATE);

  --������־
  PROCEDURE p_save_monitorlog(p_hanldetime DATE,
                              p_handlefrom VARCHAR2,
                              p_handlecmd  VARCHAR2,
                              p_handleto   VARCHAR2);

  --��û���ŵ�������Ϣ��ʶ���¼ת�쳣
  PROCEDURE p_update_expadd(p_dock_id NUMBER, p_user_id VARCHAR2);

  --ҵ���� ����Ϣ
  PROCEDURE p_select_bllmonitor(ret_cursor OUT mycursor);

  --ҵ���� ʶ����
  PROCEDURE p_select_bllmonitorrate(ret_cursor OUT mycursor);

END pkg_ocr;
/

prompt
prompt Creating package PKG_OCRX
prompt =========================
prompt
CREATE OR REPLACE PACKAGE OCR.pkg_ocrx IS

  -- Author  : YEJT
  -- Created : 2016/4/5 10:34:55
  -- Purpose : 

  -- Public type declarations
  TYPE mycursor IS REF CURSOR;

  --����������˾����
  PROCEDURE p_select_company(ret_value OUT mycursor);

  --��������˾����
  PROCEDURE p_insert_company(p_companycode VARCHAR2,
                             p_companyname VARCHAR2,
                             p_createdby   VARCHAR2);

  --ɾ������˾����
  PROCEDURE p_delete_company(p_companycode VARCHAR2, p_updatedby VARCHAR2);

  --������˾�����ѯ
  PROCEDURE p_select_companybycode(ret_value     OUT mycursor,
                                   p_companycode VARCHAR2);

  --����������˾�û�
  PROCEDURE p_select_users(ret_value OUT mycursor);

  --���û�������
  PROCEDURE p_select_user(ret_value OUT mycursor, p_userid VARCHAR2);

  --��������˾�û�
  PROCEDURE p_insert_users(p_userid      VARCHAR2,
                           p_companycode VARCHAR2,
                           p_createdby   VARCHAR2);

  --ɾ������˾�û�
  PROCEDURE p_delete_users(p_userid      VARCHAR2,
                           p_companycode VARCHAR2,
                           p_updatedby   VARCHAR2);

  --�����а��ַַ�����
  PROCEDURE p_select_vessel(ret_value OUT mycursor);

  --�������ַַ�����
  PROCEDURE p_insert_vessel(p_companycode VARCHAR2,
                            p_servicecode VARCHAR2,
                            p_linecode    VARCHAR2,
                            p_createdby   VARCHAR2);

  --���°��ַַ�����
  PROCEDURE p_update_vessel(p_companycode VARCHAR2,
                            p_servicecode VARCHAR2,
                            p_linecode    VARCHAR2,
                            p_updatedby   VARCHAR2);

  --ɾ�����ַַ�����
  PROCEDURE p_delete_vessel(p_companycode VARCHAR2,
                            p_servicecode VARCHAR2,
                            p_updatedby   VARCHAR2);

  --�����߲���ַַ�����
  PROCEDURE p_select_vesselbyservice(ret_value     OUT mycursor,
                                     p_servicecode VARCHAR2);

  --�����в����ַ�����
  PROCEDURE p_select_barge(ret_value OUT mycursor, p_companycode VARCHAR2);

  --���������ַ�����
  PROCEDURE p_insert_barge(p_companycode     VARCHAR2,
                           p_berthplanno     NUMBER,
                           p_ship_code       VARCHAR2,
                           p_in_voyage_code  VARCHAR2,
                           p_out_voyage_code VARCHAR2,
                           p_createdby       VARCHAR2);

  --ɾ�������ַ�����
  PROCEDURE p_delete_barge(p_companycode VARCHAR2,
                           p_berthplanno NUMBER,
                           p_updatedby   VARCHAR2);

  --����ַ��ļ�¼
  PROCEDURE p_select_dispatch(ret_value OUT mycursor);

  --����ַ��ļ�¼ �ɹ�ȷ����������ʶ���¼
  PROCEDURE p_select_dispatch2(ret_value OUT mycursor);

  --����ַ��ļ�¼ �ɹ�ȷ����������ʶ���¼
  PROCEDURE p_select_dispatch3(ret_value OUT mycursor);

  --�ַ�ʶ���¼
  PROCEDURE p_insert_cntx(p_dock_id         NUMBER,
                          p_container_no    VARCHAR2,
                          p_container_shape VARCHAR2,
                          p_container_size  NUMBER,
                          p_ctype           NUMBER,
                          p_dock_status     NUMBER,
                          p_cstatus         NUMBER,
                          p_trvalcrane_id   NUMBER,
                          p_trval_no        VARCHAR2,
                          p_cnam            VARCHAR2,
                          p_enam            VARCHAR2,
                          p_ship_code       VARCHAR2,
                          p_c_voyage        VARCHAR2,
                          p_pic_num         NUMBER,
                          p_msg_index       NUMBER,
                          p_ctime           DATE,
                          p_pms_id          NUMBER,
                          p_berth_num       VARCHAR2,
                          p_isarchived      VARCHAR2,
                          p_containerid     NUMBER,
                          p_linecode        VARCHAR2,
                          p_companycode     VARCHAR2,
                          p_servicecode     VARCHAR2,
                          p_shipagent       VARCHAR2,
                          p_shipowner       VARCHAR2);

  --����ʶ���¼�ַ����
  PROCEDURE p_update_dispatcthed(p_dock_id     NUMBER,
                                 p_linecode    VARCHAR2,
                                 p_companycode VARCHAR2);

  --����һ��ʶ����
  PROCEDURE p_select_nextrecord(ret_value     OUT mycursor,
                                p_username    VARCHAR2,
                                p_companycode VARCHAR2);

  --��ʣ���¼��
  PROCEDURE p_select_left(ret_value OUT mycursor, p_companycode VARCHAR2 default null);

  --����ʶ���¼������
  PROCEDURE p_update_cntstatus2(p_dock_id          NUMBER,
                                p_cstatus          NUMBER,
                                p_operatorname     VARCHAR2,
                                p_containerid      NUMBER,
                                p_ctoserrormsg     VARCHAR2,
                                p_ctoserrorcode    VARCHAR2,
                                p_isarchived       VARCHAR2,
                                p_rcontainer_no    VARCHAR2,
                                p_rcontainer_shape VARCHAR2,
                                p_dock_status      NUMBER,
                                p_isbind           VARCHAR2,
                                p_maincontainerno  VARCHAR2,
                                p_isdamage         VARCHAR2,
                                p_dmg              VARCHAR2,
                                p_isovertop        VARCHAR2,
                                p_ovr              NUMBER,
                                p_ovl              NUMBER,
                                p_ovh              NUMBER,
                                p_ovf              NUMBER,
                                p_ova              NUMBER,
                                p_isoverdis        VARCHAR2,
                                p_isimdg           VARCHAR2,
                                p_imdg1            VARCHAR2,
                                p_imdg2            VARCHAR2,
                                p_imdg3            VARCHAR2,
                                p_emptyfull        VARCHAR2,
                                p_bindseq          NUMBER,
                                p_linecode         VARCHAR2,
                                p_starttime        DATE,
                                p_container_size   NUMBER,
                                p_container_height NUMBER,
                                p_containertype    VARCHAR2,
                                p_inaim            VARCHAR2);

  --����ʶ���¼������
  PROCEDURE p_update_cntstatus3(p_dock_id          NUMBER,
                                p_cstatus          NUMBER,
                                p_operatorname     VARCHAR2,
                                p_containerid      NUMBER,
                                p_ctoserrormsg     VARCHAR2,
                                p_ctoserrorcode    VARCHAR2,
                                p_isarchived       VARCHAR2,
                                p_rcontainer_no    VARCHAR2,
                                p_rcontainer_shape VARCHAR2,
                                p_dock_status      NUMBER,
                                p_isbind           VARCHAR2,
                                p_maincontainerno  VARCHAR2,
                                p_isdamage         VARCHAR2,
                                p_dmg              VARCHAR2,
                                p_isovertop        VARCHAR2,
                                p_ovr              NUMBER,
                                p_ovl              NUMBER,
                                p_ovh              NUMBER,
                                p_ovf              NUMBER,
                                p_ova              NUMBER,
                                p_isoverdis        VARCHAR2,
                                p_isimdg           VARCHAR2,
                                p_imdg1            VARCHAR2,
                                p_imdg2            VARCHAR2,
                                p_imdg3            VARCHAR2,
                                p_emptyfull        VARCHAR2,
                                p_bindseq          NUMBER,
                                p_linecode         VARCHAR2,
                                p_starttime        DATE,
                                p_container_size   NUMBER,
                                p_container_height NUMBER,
                                p_containertype    VARCHAR2,
                                p_inaim            VARCHAR2,
                                p_enam             VARCHAR2,
                                p_ship_code        VARCHAR2,
                                p_c_voyage         VARCHAR2,
                                p_companycode      VARCHAR2);

  --���Ӧ����Ƭ��¼
  PROCEDURE p_select_photo(ret_value OUT mycursor, p_dock_id NUMBER);

  --����һ��seq_ocrxֵ
  PROCEDURE p_select_seq_ocrx(ret_value OUT mycursor);

  --����ͼƬ
  PROCEDURE p_copy_photos(p_dock_id1 NUMBER, p_dock_id2 NUMBER);

  --��ѯ
  PROCEDURE p_select_cnt(ret_cursor    OUT mycursor,
                         p_from        DATE,
                         p_to          DATE,
                         p_isarchived  VARCHAR2,
                         p_qcno        VARCHAR2,
                         p_dock_status INTEGER,
                         p_cstatus     INTEGER,
                         p_containerno VARCHAR2,
                         p_companycode VARCHAR2);

  --��������������˾�����ѯ��ҵ�嵥
  PROCEDURE p_select_report(ret_cursor    OUT mycursor,
                            p_ename       VARCHAR2,
                            p_companycode VARCHAR2);

  --����Ϣ���
  PROCEDURE p_select_cntmonitor(ret_cursor    OUT mycursor,
                                p_companycode VARCHAR2);

  --�鿴qc���
  PROCEDURE p_select_qcmonitor(ret_value     OUT mycursor,
                               p_companycode VARCHAR2);

  --�鿴δ������쳣
  PROCEDURE p_select_excep(ret_value OUT mycursor, p_companycode VARCHAR2);

  --�����쳣������
  PROCEDURE p_update_excepstatus(p_dock_id    NUMBER,
                                 p_cstatus    NUMBER,
                                 p_excepuser  VARCHAR2,
                                 p_isarchived VARCHAR2);

  --�麽�ߴ���
  PROCEDURE p_select_service(ret_value OUT mycursor);

  --���������鲵���ַ�����
  PROCEDURE p_select_bargebyvelaliase(ret_value   OUT mycursor,
                                      p_velaliase VARCHAR2);

  --�����Ƿ�ȷ�Ϲ�
  PROCEDURE p_select_cntconfirmed(ret_value   OUT mycursor,
                                  p_velaliase VARCHAR2,
                                  p_contno    VARCHAR2,
                                  p_inout     NUMBER);

  --���Ƿ��������ж
  PROCEDURE p_select_overdiscnt(ret_value     OUT mycursor,
                                p_containerid NUMBER);

END pkg_ocrx;
/

prompt
prompt Creating package PKG_OCR_CTOS
prompt =============================
prompt
CREATE OR REPLACE PACKAGE OCR.pkg_ocr_ctos IS

  -- Author  : YEJT
  -- Created : 2016/4/6 15:11:51
  -- Purpose : 

  -- Public type declarations
  TYPE mycursor IS REF CURSOR;

  --��а���
  PROCEDURE p_select_contractor(ret_value OUT mycursor);

  --���а��̲�ָ����
  PROCEDURE p_select_commend(ret_value OUT mycursor, p_contractor VARCHAR2);

  --�鰶��˾��
  PROCEDURE p_select_driver(ret_value OUT mycursor);

  --�鼤��Ĵ�������
  PROCEDURE p_select_berthplanno(ret_value OUT mycursor);

  --�鼤��Ĵ������� ����
  PROCEDURE p_select_berthplannobg(ret_value OUT mycursor);

  --�������ֳ��ն˺�
  PROCEDURE p_select_remote(ret_value OUT mycursor);

  --������Ϣ
  PROCEDURE p_select_cnt(ret_value OUT mycursor, p_cnt VARCHAR2);

  --�������뺬��
  PROCEDURE p_select_errcode(ret_value OUT mycursor, p_errcode NUMBER);

  --��isocode
  PROCEDURE p_select_isocode(ret_value OUT mycursor, p_isocode VARCHAR2);

  --���ϳ���ҵ���
  PROCEDURE p_select_truck(ret_value OUT mycursor, p_truckno VARCHAR2);

  --���ϳ���ҵ���
  PROCEDURE p_select_truck2(ret_value  OUT mycursor,
                            p_truckno  VARCHAR2,
                            p_truckseq VARCHAR2);

  --�����ϴ�װж��ʱ��
  PROCEDURE p_select_cnttime(ret_value OUT mycursor,
                             p_cntno   VARCHAR2,
                             p_isload  VARCHAR2);

  --�鳵��λ��
  PROCEDURE p_select_posontruck(ret_value     OUT mycursor,
                                p_containerid NUMBER);

  --���������麽�ߺʹ���
  PROCEDURE p_select_service(ret_value OUT mycursor, p_velaliase VARCHAR2);

END pkg_ocr_ctos;
/

prompt
prompt Creating package PKG_OCR_JOB
prompt ============================
prompt
CREATE OR REPLACE PACKAGE OCR.pkg_ocr_job IS

  -- Author  : YEJT
  -- Created : 2016/4/5 10:34:55
  -- Purpose : 

  -- Public type declarations
  TYPE mycursor IS REF CURSOR;

  --���洬�ڼ����Ϣ
  PROCEDURE p_save_vs;

  --�����ŵ������Ϣ 10�뱣��һ��
  PROCEDURE p_save_qc;

END pkg_ocr_job;
/


spool off
