-------------------------------------------
-- Export file for user OCR              --
-- Created by yejt on 2018/1/2, 12:23:02 --
-------------------------------------------

spool 表20180102.log

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
  is '驳船分发规则';
comment on column OCR.T_OCRX_BARGE.COMPANYCODE
  is '外理公司代码';
comment on column OCR.T_OCRX_BARGE.CREATEDBY
  is '创建人';
comment on column OCR.T_OCRX_BARGE.CREATETIME
  is '创建时间';
comment on column OCR.T_OCRX_BARGE.UPDATEDBY
  is '更新人';
comment on column OCR.T_OCRX_BARGE.UPDATETIME
  is '更新时间';
comment on column OCR.T_OCRX_BARGE.BERTHPLANNO
  is '船期ID';
comment on column OCR.T_OCRX_BARGE.SHIP_CODE
  is '船名代码';
comment on column OCR.T_OCRX_BARGE.IN_VOYAGE_CODE
  is '进口航次';
comment on column OCR.T_OCRX_BARGE.OUT_VOYAGE_CODE
  is '出口航次';
comment on column OCR.T_OCRX_BARGE.ENAME
  is '船别名';
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
  is '外理装卸柜号表';
comment on column OCR.T_OCRX_CNT.DOCK_ID
  is '集装箱ID';
comment on column OCR.T_OCRX_CNT.ISARCHIVED
  is '是否归档,''N''当前分区/Y临时历史分区 /A归档分区';
comment on column OCR.T_OCRX_CNT.CONTAINER_NO
  is '终端识别箱号，未识别情况下为“未识别”';
comment on column OCR.T_OCRX_CNT.RCONTAINER_NO
  is '记录修改以后的箱号,用于统计识别率';
comment on column OCR.T_OCRX_CNT.CONTAINER_SHAPE
  is '箱型，对应终端识别ISO';
comment on column OCR.T_OCRX_CNT.RCONTAINER_SHAPE
  is '修改后箱型，用于统计识别率';
comment on column OCR.T_OCRX_CNT.CONTAINER_SIZE
  is '箱尺寸，大箱40，小箱20';
comment on column OCR.T_OCRX_CNT.CTYPE
  is '中端识别单双箱，0为单箱，1为双箱';
comment on column OCR.T_OCRX_CNT.DOCK_STATUS
  is '装卸状态，0为装船，1为卸船，终端写入';
comment on column OCR.T_OCRX_CNT.CSTATUS
  is '箱子处理状态，-1/装船未自动处理, 0/准备人工处理,1/已人工获取,2/已成功处理 3/转异常 4/异常处理完成';
comment on column OCR.T_OCRX_CNT.TRVALCRANE_ID
  is '桥吊ID，平台配置好桥吊对应终端后，对应终端的数据会带上桥吊ID，用于标识该箱是哪个桥吊处理';
comment on column OCR.T_OCRX_CNT.TRVAL_NO
  is '桥吊号';
comment on column OCR.T_OCRX_CNT.CNAM
  is '中文船名';
comment on column OCR.T_OCRX_CNT.ENAM
  is '船别名';
comment on column OCR.T_OCRX_CNT.SHIP_CODE
  is '船名代码';
comment on column OCR.T_OCRX_CNT.C_VOYAGE
  is '航次';
comment on column OCR.T_OCRX_CNT.PIC_NUM
  is '箱子对应图片张数';
comment on column OCR.T_OCRX_CNT.MSG_INDEX
  is '消息id，终端上传，双吊下两条数据相同';
comment on column OCR.T_OCRX_CNT.CTIME
  is '创建时间 --OCR';
comment on column OCR.T_OCRX_CNT.PMS_ID
  is '图片服务器Id';
comment on column OCR.T_OCRX_CNT.COPYTIME
  is '同步复制时间';
comment on column OCR.T_OCRX_CNT.STARTTIME
  is '操作员获取图片时间';
comment on column OCR.T_OCRX_CNT.FINISHTIME
  is '操作员完成图片处理时间';
comment on column OCR.T_OCRX_CNT.OPERATORNAME
  is '操作员';
comment on column OCR.T_OCRX_CNT.BERTH_NUM
  is '泊位号';
comment on column OCR.T_OCRX_CNT.CONTAINERID
  is '箱ID';
comment on column OCR.T_OCRX_CNT.CTOSERRORMSG
  is 'CTOS错误信息';
comment on column OCR.T_OCRX_CNT.CTOSERRORCODE
  is 'CTOS错误代码';
comment on column OCR.T_OCRX_CNT.EXCEPSTARTIME
  is '异常处理开始时间';
comment on column OCR.T_OCRX_CNT.EXCEPFINISHTIME
  is '异常处理结果时间';
comment on column OCR.T_OCRX_CNT.EXCEPUSER
  is '异常处理人';
comment on column OCR.T_OCRX_CNT.LINECODE
  is '箱主';
comment on column OCR.T_OCRX_CNT.COMPANYCODE
  is '外理公司代理';
comment on column OCR.T_OCRX_CNT.SERVICECODE
  is '航线代码';
comment on column OCR.T_OCRX_CNT.SHIPAGENT
  is '船代';
comment on column OCR.T_OCRX_CNT.SHIPOWNER
  is '船东';
comment on column OCR.T_OCRX_CNT.ISBIND
  is '是否打捆柜';
comment on column OCR.T_OCRX_CNT.MAINCONTAINERNO
  is '主箱号';
comment on column OCR.T_OCRX_CNT.BINDSEQ
  is '打捆顺序';
comment on column OCR.T_OCRX_CNT.ISDAMAGE
  is '是否残损';
comment on column OCR.T_OCRX_CNT.DMG
  is '残损说明';
comment on column OCR.T_OCRX_CNT.ISOVERTOP
  is '是否三超';
comment on column OCR.T_OCRX_CNT.OVR
  is '右超';
comment on column OCR.T_OCRX_CNT.OVL
  is '左超';
comment on column OCR.T_OCRX_CNT.OVH
  is '超高';
comment on column OCR.T_OCRX_CNT.OVF
  is '前超';
comment on column OCR.T_OCRX_CNT.OVA
  is '后超';
comment on column OCR.T_OCRX_CNT.ISOVERDIS
  is '是否溢卸';
comment on column OCR.T_OCRX_CNT.ISIMDG
  is '是否危柜';
comment on column OCR.T_OCRX_CNT.IMDG1
  is '危标等级1';
comment on column OCR.T_OCRX_CNT.IMDG2
  is '危标等级2';
comment on column OCR.T_OCRX_CNT.IMDG3
  is '危标等级3';
comment on column OCR.T_OCRX_CNT.EMPTYFULL
  is '空重';
comment on column OCR.T_OCRX_CNT.CONTAINERTYPE
  is '箱型';
comment on column OCR.T_OCRX_CNT.CONTAINER_HEIGHT
  is '箱高';
comment on column OCR.T_OCRX_CNT.INAIM
  is '进港类型';
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
  is '外理公司代码';
comment on column OCR.T_OCRX_CO.COMPANYCODE
  is '外理公司代码';
comment on column OCR.T_OCRX_CO.COMPANYNAME
  is '外理公司名称';
comment on column OCR.T_OCRX_CO.CREATEDBY
  is '创建人';
comment on column OCR.T_OCRX_CO.CREATETIME
  is '创建时间';
comment on column OCR.T_OCRX_CO.UPDATEDBY
  is '更新人';
comment on column OCR.T_OCRX_CO.UPDATETIME
  is '更新时间';
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
  is '外理公司用户关系';
comment on column OCR.T_OCRX_USERS.USERID
  is '用户ID';
comment on column OCR.T_OCRX_USERS.COMPANYCODE
  is '外理公司代码';
comment on column OCR.T_OCRX_USERS.CREATEDBY
  is '创建人';
comment on column OCR.T_OCRX_USERS.CREATETIME
  is '创建时间';
comment on column OCR.T_OCRX_USERS.UPDATEDBY
  is '更新人';
comment on column OCR.T_OCRX_USERS.UPDATETIME
  is '更新时间';
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
  is '班轮数据分发规则';
comment on column OCR.T_OCRX_VESSEL.COMPANYCODE
  is '外理公司代码';
comment on column OCR.T_OCRX_VESSEL.SERVICECODE
  is '航线代码';
comment on column OCR.T_OCRX_VESSEL.LINECODE
  is '箱主列表';
comment on column OCR.T_OCRX_VESSEL.CREATEDBY
  is '创建人';
comment on column OCR.T_OCRX_VESSEL.CREATETIME
  is '创建时间';
comment on column OCR.T_OCRX_VESSEL.UPDATEDBY
  is '更新人';
comment on column OCR.T_OCRX_VESSEL.UPDATETIME
  is '更新时间';
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
  is '集装箱信息表';
comment on column OCR.T_OCR_CNT.DOCK_ID
  is '集装箱ID';
comment on column OCR.T_OCR_CNT.ISARCHIVED
  is '是否归档,''N''当前分区/Y临时历史分区 /A归档分区';
comment on column OCR.T_OCR_CNT.CONTAINER_NO
  is '终端识别箱号，未识别情况下为“未识别”';
comment on column OCR.T_OCR_CNT.RCONTAINER_NO
  is '记录修改以后的箱号,用于统计识别率';
comment on column OCR.T_OCR_CNT.CONTAINER_SHAPE
  is '箱型，对应终端识别ISO';
comment on column OCR.T_OCR_CNT.RCONTAINER_SHAPE
  is '修改后箱型，用于统计识别率';
comment on column OCR.T_OCR_CNT.CONTAINER_SIZE
  is '箱尺寸，大箱40，小箱20';
comment on column OCR.T_OCR_CNT.CONTAINER_HEIGHT
  is '高度';
comment on column OCR.T_OCR_CNT.PLATE_NO
  is '拖车号，终端识别';
comment on column OCR.T_OCR_CNT.RPALGE_NO
  is '修改后推车号，用于统计识别率';
comment on column OCR.T_OCR_CNT.CTYPE
  is '中端识别单双箱，0为单箱，1为双箱';
comment on column OCR.T_OCR_CNT.CWEIGT
  is '重量，终端写入';
comment on column OCR.T_OCR_CNT.DOCK_STATUS
  is '装卸状态，0为装船，1为卸船，终端写入';
comment on column OCR.T_OCR_CNT.CONTAINER_POS
  is '箱位，可能写入的值：双箱下：F，A，单箱默认M，通过港口理货服务器分析终端抓拍图片写入数据库，可能由于图片识别不满足要求计算不出';
comment on column OCR.T_OCR_CNT.CSTATUS
  is '箱子处理状态，-4/仅外理作业 -3/重复记录 -2/无QC作业设置 -1/装船未自动处理, 0/准备人工处理,1/已人工获取,2/已成功处理 3/转异常 4/异常处理完成';
comment on column OCR.T_OCR_CNT.CONFIDENCE
  is '置信度，终端传入';
comment on column OCR.T_OCR_CNT.TRVALCRANE_ID
  is '桥吊ID，平台配置好桥吊对应终端后，对应终端的数据会带上桥吊ID，用于标识该箱是哪个桥吊处理';
comment on column OCR.T_OCR_CNT.TRVAL_NO
  is '桥吊号';
comment on column OCR.T_OCR_CNT.DRIVER_NO
  is '桥司机号，处理该箱子的司机编号，对应桥吊配置表里面的配置';
comment on column OCR.T_OCR_CNT.LANE_NO
  is '车道号，终端识别';
comment on column OCR.T_OCR_CNT.BEGIN_TIME
  is '开始处理时间，终端传入';
comment on column OCR.T_OCR_CNT.END_TIME
  is '结束处理时间，终端写入';
comment on column OCR.T_OCR_CNT.USER_ID
  is '用户ID';
comment on column OCR.T_OCR_CNT.CNAM
  is '中文船名';
comment on column OCR.T_OCR_CNT.ENAM
  is '船别名';
comment on column OCR.T_OCR_CNT.SHIP_CODE
  is '船名代码';
comment on column OCR.T_OCR_CNT.C_VOYAGE
  is '航次';
comment on column OCR.T_OCR_CNT.PIC_NUM
  is '箱子对应图片张数';
comment on column OCR.T_OCR_CNT.MSG_INDEX
  is '消息id，终端上传，双吊下两条数据相同';
comment on column OCR.T_OCR_CNT.CARCONT
  is '车头方向，用于双箱下判断图片位置，由ptas计算写入，有计算不出的可能。默认0,1代表朝左，2代表朝右';
comment on column OCR.T_OCR_CNT.GANGS
  is '岸边理货登录名';
comment on column OCR.T_OCR_CNT.CONTAINER_DIR
  is '箱门方向';
comment on column OCR.T_OCR_CNT.STREAM_DIR
  is '流向';
comment on column OCR.T_OCR_CNT.LOADING_PORT
  is '装船港';
comment on column OCR.T_OCR_CNT.UNLOADING_PORT
  is '卸船港';
comment on column OCR.T_OCR_CNT.DEST_PORT
  is '目的港';
comment on column OCR.T_OCR_CNT.BAY_HORIZONTAL
  is '坐标X';
comment on column OCR.T_OCR_CNT.BAY_VERTICAL
  is '坐标Y';
comment on column OCR.T_OCR_CNT.BAY
  is 'Bay位';
comment on column OCR.T_OCR_CNT.CTIME
  is '创建时间 --OCR';
comment on column OCR.T_OCR_CNT.PMS_ID
  is '图片服务器Id';
comment on column OCR.T_OCR_CNT.COPYTIME
  is '同步复制时间';
comment on column OCR.T_OCR_CNT.STARTTIME
  is '操作员获取图片时间';
comment on column OCR.T_OCR_CNT.FINISHTIME
  is '操作员完成图片处理时间';
comment on column OCR.T_OCR_CNT.OPERATORNAME
  is '操作员';
comment on column OCR.T_OCR_CNT.COMMEND_ID
  is '指挥手工号';
comment on column OCR.T_OCR_CNT.CONTRACTOR_CODE
  is '承包商代码';
comment on column OCR.T_OCR_CNT.BERTH_NUM
  is '泊位号';
comment on column OCR.T_OCR_CNT.CONTAINERID
  is '箱ID';
comment on column OCR.T_OCR_CNT.CTOSERRORMSG
  is 'CTOS错误信息';
comment on column OCR.T_OCR_CNT.CTOSERRORCODE
  is 'CTOS错误代码';
comment on column OCR.T_OCR_CNT.EXCEPSTARTIME
  is '异常处理开始时间';
comment on column OCR.T_OCR_CNT.EXCEPFINISHTIME
  is '异常处理结果时间';
comment on column OCR.T_OCR_CNT.EXCEPUSER
  is '异常处理人';
comment on column OCR.T_OCR_CNT.DEVICEOPTIMELINESID
  is 'MOVE ID';
comment on column OCR.T_OCR_CNT.LINECODE
  is '箱主';
comment on column OCR.T_OCR_CNT.DISPATCHEDTO
  is '外理公司代码';
comment on column OCR.T_OCR_CNT.SERVICECODE
  is '航线代码';
comment on column OCR.T_OCR_CNT.SHIPAGENT
  is '船代';
comment on column OCR.T_OCR_CNT.SHIPOWNER
  is '船东';
comment on column OCR.T_OCR_CNT.DISPATCHTIME
  is '分发给外理的时间';
comment on column OCR.T_OCR_CNT.PLC_OPEN_TIME
  is 'plc开锁时间';
comment on column OCR.T_OCR_CNT.PLC_CLOSE_TIME
  is 'plc闭锁时间';
comment on column OCR.T_OCR_CNT.IDENT_START_TIME
  is '识别开始时间';
comment on column OCR.T_OCR_CNT.IDENT_END_TIME
  is '识别结束时间';
comment on column OCR.T_OCR_CNT.CHECK_FLAG
  is '卸柜验箱标志 0未验/1正在验/2已验';
comment on column OCR.T_OCR_CNT.DMG_CODE
  is '残损代码';
comment on column OCR.T_OCR_CNT.CHECK_START_TIME
  is '验箱开始时间';
comment on column OCR.T_OCR_CNT.CHECK_END_TIME
  is '验箱结束时间';
comment on column OCR.T_OCR_CNT.CHECKEDBY
  is '验箱员';
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
  is '错误日志';
comment on column OCR.T_OCR_ERRORLOG.QCNO
  is '桥号';
comment on column OCR.T_OCR_ERRORLOG.DEVICTTYPE
  is '设备类型';
comment on column OCR.T_OCR_ERRORLOG.DEVICENAME
  is '设备名';
comment on column OCR.T_OCR_ERRORLOG.ERRORTYPE
  is '错误类型';
comment on column OCR.T_OCR_ERRORLOG.STARTTIME
  is '开始时间';
comment on column OCR.T_OCR_ERRORLOG.ENDTIME
  is '结束时间';
comment on column OCR.T_OCR_ERRORLOG.ERRORMSG
  is '错误信息';
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
  is '运维知识库';
comment on column OCR.T_OCR_EXP.SEQNO
  is '序号';
comment on column OCR.T_OCR_EXP.APPNAME
  is '应用名称';
comment on column OCR.T_OCR_EXP.SYMPTOM
  is '故障现象';
comment on column OCR.T_OCR_EXP.SOLUTION
  is '解决办法';
comment on column OCR.T_OCR_EXP.INPUTEDBY
  is '录入人';
comment on column OCR.T_OCR_EXP.INPUTETIME
  is '录入时间';
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
  is '操作日志';
comment on column OCR.T_OCR_LOG.LOGID
  is '日志ID';
comment on column OCR.T_OCR_LOG.TABLENAME
  is '更新表';
comment on column OCR.T_OCR_LOG.COLNAME
  is '列名，或关键值名';
comment on column OCR.T_OCR_LOG.OLDVALUE
  is '旧值';
comment on column OCR.T_OCR_LOG.NEWVALUE
  is '新值';
comment on column OCR.T_OCR_LOG.UPDATEDBY
  is '更新人';
comment on column OCR.T_OCR_LOG.UPDATETIME
  is '更新时间';
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
  is 'MAC地址表';
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
  is '监控操作日志';
comment on column OCR.T_OCR_MONITORLOG.HANLDETIME
  is '操作时间';
comment on column OCR.T_OCR_MONITORLOG.HANDLEFROM
  is '操作机器';
comment on column OCR.T_OCR_MONITORLOG.HANDLECMD
  is '操作命令';
comment on column OCR.T_OCR_MONITORLOG.HANDLETO
  is '目标机器';
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
  is '系统参数';
comment on column OCR.T_OCR_PARAMS.PARAMNAME
  is '参数名';
comment on column OCR.T_OCR_PARAMS.PARAMVALUE
  is '参数值';
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
  is '图片';
comment on column OCR.T_OCR_PHOTO.PHOTO_ID
  is 'id（序列）';
comment on column OCR.T_OCR_PHOTO.ISARCHIVED
  is '是否归档';
comment on column OCR.T_OCR_PHOTO.DOCK_ID
  is '箱子id表4中箱子id';
comment on column OCR.T_OCR_PHOTO.PHOTO_URL
  is '图片url';
comment on column OCR.T_OCR_PHOTO.CTIME
  is '创建时间 OCR';
comment on column OCR.T_OCR_PHOTO.PHOTO_NAME
  is '图片名称';
comment on column OCR.T_OCR_PHOTO.PHOTO_POS
  is '图片位置';
comment on column OCR.T_OCR_PHOTO.CONT_ORDER
  is '箱子顺序';
comment on column OCR.T_OCR_PHOTO.COPYTIME
  is '同步复制时间';
comment on column OCR.T_OCR_PHOTO.DATA_DELAY
  is 'plc延迟';
comment on column OCR.T_OCR_PHOTO.PLCDATA_BOXHEIGHT
  is '拍照高度';
comment on column OCR.T_OCR_PHOTO.PLCDATA_BOXDISPLACEMENT
  is '拍照横坐标';
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
  WORKTYPE          VARCHAR2(10) default '内理',
  CAMERACODE        VARCHAR2(50)
)
;
comment on table OCR.T_OCR_QCSET
  is '桥吊作业配置';
comment on column OCR.T_OCR_QCSET.WORKSET_ID
  is '桥吊作业配置id';
comment on column OCR.T_OCR_QCSET.TRVALCRANE_ID
  is '桥吊id表1中trvalcrane_id';
comment on column OCR.T_OCR_QCSET.TRVALCRANE_NO
  is '桥吊no表1中trvalcrane_no';
comment on column OCR.T_OCR_QCSET.CONTRACTOR_CODE
  is '承包商代码';
comment on column OCR.T_OCR_QCSET.COMMEND_ID
  is '指挥手工号';
comment on column OCR.T_OCR_QCSET.DRIVER_ID
  is 'QC司机工号';
comment on column OCR.T_OCR_QCSET.SHIP_CODE
  is '船名代码';
comment on column OCR.T_OCR_QCSET.IN_VOYAGE_CODE
  is '进口航次';
comment on column OCR.T_OCR_QCSET.OUT_VOYAGE_CODE
  is '出口航次';
comment on column OCR.T_OCR_QCSET.BERTH_NUM
  is '泊位号';
comment on column OCR.T_OCR_QCSET.VOYAGE_TYPE
  is '进出口类型，IM表示进口，EX表示出口';
comment on column OCR.T_OCR_QCSET.SHIPMENT_DEAL
  is '装船人工处理开关， 开/关';
comment on column OCR.T_OCR_QCSET.OPERATOR_UID
  is '操作员id';
comment on column OCR.T_OCR_QCSET.COMMEND_PAW
  is '指挥手登陆密码';
comment on column OCR.T_OCR_QCSET.TERMINAL_NO
  is '设备终端号';
comment on column OCR.T_OCR_QCSET.TICKET_ID
  is '指挥手登陆成功后返回的会话编号';
comment on column OCR.T_OCR_QCSET.DEVICE_NO
  is '指挥手登陆成功后返回的设备号';
comment on column OCR.T_OCR_QCSET.DEVICE_TYPE
  is '指挥手登陆成功后返回的设备类型';
comment on column OCR.T_OCR_QCSET.IS_CHANGE
  is '此字段主要是为了控制控件重新初始化，只有桥吊作业配置修改过了，才重新初始化控件';
comment on column OCR.T_OCR_QCSET.STATUS
  is '桥吊作业配置状态，停止作业、暂停作业、作业中';
comment on column OCR.T_OCR_QCSET.ERROR_MESS
  is 'Ctos返回的信息';
comment on column OCR.T_OCR_QCSET.CREATE_TIME
  is '创建时间';
comment on column OCR.T_OCR_QCSET.UPDATE_TIME
  is '更新时间';
comment on column OCR.T_OCR_QCSET.BERTH_WAY
  is '靠泊方向 R/L';
comment on column OCR.T_OCR_QCSET.VESSELALIASE
  is '船别名';
comment on column OCR.T_OCR_QCSET.BERTHPLANNO
  is '船期ID';
comment on column OCR.T_OCR_QCSET.AVESSELNAME
  is '船英文名';
comment on column OCR.T_OCR_QCSET.INAGENT
  is '进口船代';
comment on column OCR.T_OCR_QCSET.OUTAGENT
  is '出口船代';
comment on column OCR.T_OCR_QCSET.OWNER
  is '船东';
comment on column OCR.T_OCR_QCSET.INVESSELLINECODE
  is '进口航线';
comment on column OCR.T_OCR_QCSET.OUTVESSELLINECODE
  is '出口航线';
comment on column OCR.T_OCR_QCSET.WORKTYPE
  is '作业模式，内理、外理';
comment on column OCR.T_OCR_QCSET.CAMERACODE
  is '全景像头编码';
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
  is '分杆统计';
comment on column OCR.T_OCR_STATIC_QC.WORKPOINTNO
  is '工作点';
comment on column OCR.T_OCR_STATIC_QC.LOADCOMPLETED
  is '装船完成';
comment on column OCR.T_OCR_STATIC_QC.DISCOMPLETED
  is '卸船完成';
comment on column OCR.T_OCR_STATIC_QC.LOADLEFT
  is '装船剩余';
comment on column OCR.T_OCR_STATIC_QC.DISLEFT
  is '卸船剩余';
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
  is '班轮作业统计表';
comment on column OCR.T_OCR_STATIC_VS.BERTHPLANNO
  is '船期ID';
comment on column OCR.T_OCR_STATIC_VS.CVESSELNAME
  is '中文船名';
comment on column OCR.T_OCR_STATIC_VS.LOADCOUNT
  is '装量';
comment on column OCR.T_OCR_STATIC_VS.DISCHARGECOUNT
  is '卸量';
comment on column OCR.T_OCR_STATIC_VS.TTL
  is '总装卸量';
comment on column OCR.T_OCR_STATIC_VS.LOADLEFT
  is '装船余量';
comment on column OCR.T_OCR_STATIC_VS.DISLEFT
  is '卸船余量';
comment on column OCR.T_OCR_STATIC_VS.BERTHNO
  is '泊位';
comment on column OCR.T_OCR_STATIC_VS.TERMINALCODE
  is '港区';
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
  is '车号提前抓拍';
comment on column OCR.T_OCR_TRUCK.QCNO
  is '岸桥号';
comment on column OCR.T_OCR_TRUCK.TRUCKNO
  is '拖车号';
comment on column OCR.T_OCR_TRUCK.CREATETIME
  is '拍照时间';
comment on column OCR.T_OCR_TRUCK.COPYTIME
  is '同步时间';
comment on column OCR.T_OCR_TRUCK.ISARCHIVED
  is '是否归档';
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

  --查当前最大ID
  PROCEDURE p_select_maxid(ret_value OUT mycursor);

  --查当前最大ID 图片
  PROCEDURE p_select_maxidphoto(ret_value OUT mycursor);

  --查当前最大ID 车号
  PROCEDURE p_select_maxidtruck(ret_value OUT mycursor);

  --插入新识别记录
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

  --插入新识别记录
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
  --插入新识别记录
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
  --插入图片记录
  PROCEDURE p_insert_photo(p_photo_id   NUMBER,
                           p_dock_id    NUMBER,
                           p_photo_url  VARCHAR2,
                           p_ctime      DATE,
                           p_photo_name VARCHAR2,
                           p_photo_pos  VARCHAR2,
                           p_cont_order NUMBER);

  --插入图片记录
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

  --插入车号记录
  PROCEDURE p_insert_truck(p_seqno      NUMBER,
                           p_qcno       VARCHAR2,
                           p_truckno    VARCHAR2,
                           p_createtime DATE);

  --查桥吊作业配置
  PROCEDURE p_select_qcset(ret_value OUT mycursor);

  --查桥吊作业配置
  PROCEDURE p_select_qcset1(ret_value OUT mycursor, p_qc VARCHAR2);

  --更新桥吊作业配置
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

  --更新桥吊作业配置
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

  --查下一条识别结果
  PROCEDURE p_select_nextrecord(ret_value  OUT mycursor,
                                p_username VARCHAR2);

  --查下一条识别结果 分港区
  PROCEDURE p_select_nextrecord2(ret_value  OUT mycursor,
                                 p_username VARCHAR2,
                                 p_terminal VARCHAR2);

  --查对应的照片记录
  PROCEDURE p_select_photo(ret_value OUT mycursor, p_dock_id NUMBER);

  --查取系统参数
  PROCEDURE p_select_params(ret_value OUT mycursor);

  --更新识别记录处理结果
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
  --更新识别记录处理结果
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

  --更新识别记录处理结果
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

  --查下一条识别结果 分港区 包括验箱数据
  PROCEDURE p_select_nextrecord3(ret_value  OUT mycursor,
                                 p_username VARCHAR2,
                                 p_terminal VARCHAR2);

  --更新识别记录验箱处理结果
  PROCEDURE p_update_cntcheck(p_dock_id    NUMBER,
                              p_isarchived VARCHAR2,
                              p_check_flag NUMBER,
                              p_dmg_code   VARCHAR2,
                              p_checkedby  VARCHAR2);

  --查需自动处理装船的记录
  PROCEDURE p_select_loadauto(ret_value OUT mycursor);

  --查需自动处理装船的记录
  PROCEDURE p_select_loadauto1(ret_value OUT mycursor, p_qc VARCHAR2);

  --查看操作日志
  PROCEDURE p_select_log(ret_value   OUT mycursor,
                         p_tablename VARCHAR2,
                         p_colname   VARCHAR2);

  --查看qc监控
  PROCEDURE p_select_qcmonitor(ret_value OUT mycursor);

  --查看拖车监控
  PROCEDURE p_select_truckmonitor(ret_value OUT mycursor);

  --归档数据
  PROCEDURE p_archive;

  --查所有未处理的异常
  PROCEDURE p_select_excep(ret_value OUT mycursor);

  --更新异常处理结果
  PROCEDURE p_update_excepstatus(p_dock_id    NUMBER,
                                 p_cstatus    NUMBER,
                                 p_excepuser  VARCHAR2,
                                 p_isarchived VARCHAR2);

  --更新异常处理结果
  PROCEDURE p_update_excepstatus2(p_dock_id       NUMBER,
                                  p_cstatus       NUMBER,
                                  p_excepuser     VARCHAR2,
                                  p_isarchived    VARCHAR2,
                                  p_rcontainer_no VARCHAR2,
                                  p_linecode      VARCHAR2,
                                  p_containerid   NUMBER);

  --查剩余记录数
  PROCEDURE p_select_left(ret_value OUT mycursor);

  --查剩余记录数 分港区
  PROCEDURE p_select_left2(ret_value OUT mycursor, p_terminal VARCHAR2);

  --班轮监控
  PROCEDURE p_select_vslmonitor(ret_cursor OUT mycursor);

  --箱信息监控
  PROCEDURE p_select_cntmonitor(ret_cursor OUT mycursor);

  --装船监控
  PROCEDURE p_select_loadmonitor(ret_cursor OUT mycursor);

  --已装船数据
  PROCEDURE p_select_loaded(ret_cursor OUT mycursor,
                            p_from     DATE,
                            p_to       DATE);

  --转异常数据
  PROCEDURE p_select_excepmonitor(ret_cursor OUT mycursor,
                                  p_from     DATE,
                                  p_to       DATE);

  --查询
  PROCEDURE p_select_cnt(ret_cursor    OUT mycursor,
                         p_from        DATE,
                         p_to          DATE,
                         p_isarchived  VARCHAR2,
                         p_qcno        VARCHAR2,
                         p_dock_status INTEGER,
                         p_cstatus     INTEGER,
                         p_containerno VARCHAR2);

  --查询
  PROCEDURE p_select_cnt2(ret_cursor    OUT mycursor,
                          p_from        DATE,
                          p_to          DATE,
                          p_isarchived  VARCHAR2,
                          p_qcno        VARCHAR2,
                          p_dock_status INTEGER,
                          p_cstatus     INTEGER,
                          p_containerno VARCHAR2,
                          p_truckno     VARCHAR2);

  --查对应的照片记录
  PROCEDURE p_select_photo2(ret_value OUT mycursor, p_dock_id NUMBER);

  --班轮监控
  PROCEDURE p_select_vslmonitor2(ret_cursor OUT mycursor);

  --查上5条作业指令 
  PROCEDURE p_select_lastfive(ret_value OUT mycursor, p_qc VARCHAR2);

  --船期桥吊监控
  PROCEDURE p_select_vslmonitor3(ret_cursor OUT mycursor);

  --更新识别记录move id
  PROCEDURE p_update_moveid(p_msg_index           NUMBER,
                            p_trval_no            VARCHAR2,
                            p_deviceoptimelinesid NUMBER);

  --箱号快查
  PROCEDURE p_select_cntquery(ret_cursor    OUT mycursor,
                              p_containerno VARCHAR2);

  PROCEDURE p_select_vslmonitor31(ret_cursor OUT mycursor);
  PROCEDURE p_select_vslmonitor21(ret_cursor OUT mycursor);

  --按桥号查箱
  PROCEDURE p_select_cntbyqcno(ret_cursor OUT mycursor, p_qcno VARCHAR2);

  --查对应的照片记录
  PROCEDURE p_select_photo3(ret_value OUT mycursor, p_photo_id NUMBER);

  --查qc上的设备
  /*
  PROCEDURE p_select_qcdevice(ret_value OUT mycursor, p_qcno VARCHAR2);
  */

  PROCEDURE p_select_qcrate(ret_value OUT mycursor,
                            p_qcno    VARCHAR2,
                            p_type    NUMBER DEFAULT 1000);

  --按桥号查箱
  PROCEDURE p_select_cntbyqcno2(ret_cursor OUT mycursor, p_qcno VARCHAR2);

  --保存错误日志
  PROCEDURE p_save_errorlog(p_qcno       VARCHAR2,
                            p_devicttype VARCHAR2,
                            p_devicename VARCHAR2,
                            p_errortype  VARCHAR2,
                            p_starttime  DATE,
                            p_errormsg   VARCHAR2,
                            p_done       VARCHAR2);

  --查错误日志
  PROCEDURE p_select_errorlog(ret_value    OUT mycursor,
                              p_qcno       VARCHAR2,
                              p_devicetype VARCHAR2,
                              p_fromtime   DATE,
                              p_endtime    DATE);

  --保存日志
  PROCEDURE p_save_monitorlog(p_hanldetime DATE,
                              p_handlefrom VARCHAR2,
                              p_handlecmd  VARCHAR2,
                              p_handleto   VARCHAR2);

  --将没有桥吊配置信息的识别记录转异常
  PROCEDURE p_update_expadd(p_dock_id NUMBER, p_user_id VARCHAR2);

  --业务监控 箱信息
  PROCEDURE p_select_bllmonitor(ret_cursor OUT mycursor);

  --业务监控 识别率
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

  --查所有外理公司代码
  PROCEDURE p_select_company(ret_value OUT mycursor);

  --新增外理公司代码
  PROCEDURE p_insert_company(p_companycode VARCHAR2,
                             p_companyname VARCHAR2,
                             p_createdby   VARCHAR2);

  --删除外理公司代码
  PROCEDURE p_delete_company(p_companycode VARCHAR2, p_updatedby VARCHAR2);

  --按外理公司代码查询
  PROCEDURE p_select_companybycode(ret_value     OUT mycursor,
                                   p_companycode VARCHAR2);

  --查所有外理公司用户
  PROCEDURE p_select_users(ret_value OUT mycursor);

  --按用户名查找
  PROCEDURE p_select_user(ret_value OUT mycursor, p_userid VARCHAR2);

  --新增外理公司用户
  PROCEDURE p_insert_users(p_userid      VARCHAR2,
                           p_companycode VARCHAR2,
                           p_createdby   VARCHAR2);

  --删除外理公司用户
  PROCEDURE p_delete_users(p_userid      VARCHAR2,
                           p_companycode VARCHAR2,
                           p_updatedby   VARCHAR2);

  --查所有班轮分发规则
  PROCEDURE p_select_vessel(ret_value OUT mycursor);

  --新增班轮分发规则
  PROCEDURE p_insert_vessel(p_companycode VARCHAR2,
                            p_servicecode VARCHAR2,
                            p_linecode    VARCHAR2,
                            p_createdby   VARCHAR2);

  --更新班轮分发规则
  PROCEDURE p_update_vessel(p_companycode VARCHAR2,
                            p_servicecode VARCHAR2,
                            p_linecode    VARCHAR2,
                            p_updatedby   VARCHAR2);

  --删除班轮分发规则
  PROCEDURE p_delete_vessel(p_companycode VARCHAR2,
                            p_servicecode VARCHAR2,
                            p_updatedby   VARCHAR2);

  --按航线查班轮分发规则
  PROCEDURE p_select_vesselbyservice(ret_value     OUT mycursor,
                                     p_servicecode VARCHAR2);

  --查所有驳船分发规则
  PROCEDURE p_select_barge(ret_value OUT mycursor, p_companycode VARCHAR2);

  --新增驳船分发规则
  PROCEDURE p_insert_barge(p_companycode     VARCHAR2,
                           p_berthplanno     NUMBER,
                           p_ship_code       VARCHAR2,
                           p_in_voyage_code  VARCHAR2,
                           p_out_voyage_code VARCHAR2,
                           p_createdby       VARCHAR2);

  --删除驳船分发规则
  PROCEDURE p_delete_barge(p_companycode VARCHAR2,
                           p_berthplanno NUMBER,
                           p_updatedby   VARCHAR2);

  --查需分发的记录
  PROCEDURE p_select_dispatch(ret_value OUT mycursor);

  --查需分发的记录 成功确认有箱主的识别记录
  PROCEDURE p_select_dispatch2(ret_value OUT mycursor);

  --查需分发的记录 成功确认无箱主的识别记录
  PROCEDURE p_select_dispatch3(ret_value OUT mycursor);

  --分发识别记录
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

  --更新识别记录分发结果
  PROCEDURE p_update_dispatcthed(p_dock_id     NUMBER,
                                 p_linecode    VARCHAR2,
                                 p_companycode VARCHAR2);

  --查下一条识别结果
  PROCEDURE p_select_nextrecord(ret_value     OUT mycursor,
                                p_username    VARCHAR2,
                                p_companycode VARCHAR2);

  --查剩余记录数
  PROCEDURE p_select_left(ret_value OUT mycursor, p_companycode VARCHAR2 default null);

  --更新识别记录处理结果
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

  --更新识别记录处理结果
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

  --查对应的照片记录
  PROCEDURE p_select_photo(ret_value OUT mycursor, p_dock_id NUMBER);

  --查下一个seq_ocrx值
  PROCEDURE p_select_seq_ocrx(ret_value OUT mycursor);

  --复制图片
  PROCEDURE p_copy_photos(p_dock_id1 NUMBER, p_dock_id2 NUMBER);

  --查询
  PROCEDURE p_select_cnt(ret_cursor    OUT mycursor,
                         p_from        DATE,
                         p_to          DATE,
                         p_isarchived  VARCHAR2,
                         p_qcno        VARCHAR2,
                         p_dock_status INTEGER,
                         p_cstatus     INTEGER,
                         p_containerno VARCHAR2,
                         p_companycode VARCHAR2);

  --按船别名和外理公司代码查询作业清单
  PROCEDURE p_select_report(ret_cursor    OUT mycursor,
                            p_ename       VARCHAR2,
                            p_companycode VARCHAR2);

  --箱信息监控
  PROCEDURE p_select_cntmonitor(ret_cursor    OUT mycursor,
                                p_companycode VARCHAR2);

  --查看qc监控
  PROCEDURE p_select_qcmonitor(ret_value     OUT mycursor,
                               p_companycode VARCHAR2);

  --查看未处理的异常
  PROCEDURE p_select_excep(ret_value OUT mycursor, p_companycode VARCHAR2);

  --更新异常处理结果
  PROCEDURE p_update_excepstatus(p_dock_id    NUMBER,
                                 p_cstatus    NUMBER,
                                 p_excepuser  VARCHAR2,
                                 p_isarchived VARCHAR2);

  --查航线代码
  PROCEDURE p_select_service(ret_value OUT mycursor);

  --按船别名查驳船分发规则
  PROCEDURE p_select_bargebyvelaliase(ret_value   OUT mycursor,
                                      p_velaliase VARCHAR2);

  --查箱是否确认过
  PROCEDURE p_select_cntconfirmed(ret_value   OUT mycursor,
                                  p_velaliase VARCHAR2,
                                  p_contno    VARCHAR2,
                                  p_inout     NUMBER);

  --查是否是箱号溢卸
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

  --查承包商
  PROCEDURE p_select_contractor(ret_value OUT mycursor);

  --按承包商查指挥手
  PROCEDURE p_select_commend(ret_value OUT mycursor, p_contractor VARCHAR2);

  --查岸桥司机
  PROCEDURE p_select_driver(ret_value OUT mycursor);

  --查激活的船名航次
  PROCEDURE p_select_berthplanno(ret_value OUT mycursor);

  --查激活的船名航次 驳船
  PROCEDURE p_select_berthplannobg(ret_value OUT mycursor);

  --查虚拟手持终端号
  PROCEDURE p_select_remote(ret_value OUT mycursor);

  --查箱信息
  PROCEDURE p_select_cnt(ret_value OUT mycursor, p_cnt VARCHAR2);

  --查错误代码含义
  PROCEDURE p_select_errcode(ret_value OUT mycursor, p_errcode NUMBER);

  --查isocode
  PROCEDURE p_select_isocode(ret_value OUT mycursor, p_isocode VARCHAR2);

  --查拖车作业情况
  PROCEDURE p_select_truck(ret_value OUT mycursor, p_truckno VARCHAR2);

  --查拖车作业情况
  PROCEDURE p_select_truck2(ret_value  OUT mycursor,
                            p_truckno  VARCHAR2,
                            p_truckseq VARCHAR2);

  --查箱上次装卸船时间
  PROCEDURE p_select_cnttime(ret_value OUT mycursor,
                             p_cntno   VARCHAR2,
                             p_isload  VARCHAR2);

  --查车上位置
  PROCEDURE p_select_posontruck(ret_value     OUT mycursor,
                                p_containerid NUMBER);

  --按船别名查航线和船型
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

  --保存船期监控信息
  PROCEDURE p_save_vs;

  --保存桥吊监控信息 10秒保存一次
  PROCEDURE p_save_qc;

END pkg_ocr_job;
/


spool off
