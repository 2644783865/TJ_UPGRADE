<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_OUT_ExportExcel.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OUT_ExportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>导出外协计划</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
 <base id="goDownload" target="_self" />
    <script language="javascript" type="text/javascript">
   function  OutExport(obj)
   {
       ////document.getElementById("btnClose").disabled=true;
       document.getElementById("imgloading").className="show";
       
       var downloadBase=document.getElementById("goDownload");
       var downloadBox=document.getElementById("download");
       download.src=document.URL;
       
       downloadBase.target="download";
       setTimeout("CancelMsg()",100);
       return true;
   }
   
    function CancelMsg()
    {
       var downloadBase=document.getElementById("goDownload");
       downloadBase.target="_self";
       document.getElementById("btnOutExport").disabled=true;
    }
    </script>
</head>
<body>

<iframe id="download" name="download" height="0px" width="0px">
</iframe>
<!--用iframe模拟文件下载-->
   <form id="form1" runat="server" style="width:100%">
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="100%">
             <tr>
               <td style="width:33%">生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
             <td style="width:33%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>工程名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
             </tr>
           </table>
       </div>
     </div>
   </div>
   
    <div class="box-wrapper" style="text-align:center;">
        <div class="box-outer">
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
          <tr>
            <td>
               
            </td>
            <td align="left">
                <asp:RadioButtonList ID="rblOutType" runat="server" AutoPostBack="true" RepeatColumns="2" RepeatDirection="Horizontal" OnSelectedIndexChanged="ddlouttype_SelectedIndexChanged">
                  <asp:ListItem Text="正常/变更计划" Value="View_TM_OUTSOURCETOTAL"></asp:ListItem>
                  <asp:ListItem Text="取消计划" Value="View_TM_OUTSCHANGERVW"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
          </tr>
          <tr>
            <td style="width:10%;" class="tdleft1"><strong>提交类别:</strong></td>
            <td align="left">
            <asp:DropDownList ID="ddlouttype" runat="server" AutoPostBack="true" onselectedindexchanged="ddlouttype_SelectedIndexChanged">
               <asp:ListItem Text="-请选择-" Value=" "></asp:ListItem>
               <asp:ListItem Text="协A" Value="协A"></asp:ListItem>
               <asp:ListItem Text="协B" Value="协B"></asp:ListItem>
            </asp:DropDownList>
            </td>
          </tr>
          
          <tr>
           <td class="tdleft1"><strong>计划批号:</strong></td>
           <td align="left">
               <asp:DropDownList ID="ddlLotNumList" runat="server">
               </asp:DropDownList>
           </td>
          </tr>
          <tr>
            <td align="right">
                <asp:Image ID="imgloading" CssClass="hidden" ImageUrl="~/Assets/images/PrgBar.gif" border="0" hspace="2" align="absmiddle" runat="server" />
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnOutExport" runat="server" Text="导出" OnClientClick="return OutExport(this);" OnClick="btnOutExport_OnClick"  />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnClose" type="button" runat="server" value="取消" onclick="window.close();" />
            </td>
          </tr>
        </table>
    </div>
    </div>
    </form>
</body>
</html>
