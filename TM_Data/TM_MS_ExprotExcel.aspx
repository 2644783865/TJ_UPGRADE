<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_ExprotExcel.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_ExprotExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>导出制作明细</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
 <base id="goDownload" target="_self" />
    <script language="javascript" type="text/javascript">
   function  MsExport(obj)
   {
       ////////document.getElementById("btnClose").disabled=true;
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
       document.getElementById("btnMsExport").disabled=true;
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
               <td style="width:33%">任务号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
             <td style="width:33%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>设备名称：
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
           <td class="tdleft1"><strong>批号:</strong></td>
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
                <asp:Button ID="btnMsExport" runat="server" Text="导出" OnClientClick="return MsExport(this);" OnClick="btnMsExport_OnClick"  />
                &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnMsAllExport" runat="server" Text="批量导出"  OnClick="btnMsAllExport_OnClick"  />
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
