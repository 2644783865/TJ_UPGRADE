<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="TM_Original_Data_Operate_sptip.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Original_Data_Operate_sptip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据修改注意事项</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-wrapper">
        <div class="box-outer">
        一、已拆分记录<br />
        &nbsp;&nbsp;&nbsp;&nbsp;在调整制作明细时，如果对总序进行了拆分，更新数据时需要注意以下几点:<br />
        （1）、修改物料编码时，程序自动完成所有拆分记录的物料替换，但重量等信息需要手动修改；<br />
        （2）、所有拆分记录自动生成变更。
        （3）、在修改拆分记录时，按总序进行查询，已便于修改。
        </div>
    </div>
    </form>
</body>
</html>
