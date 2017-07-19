<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseStorage_Export.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseStorage_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
    <base target="download" />
    
    
    <script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>
    <script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>
    <script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>
    <script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>
    <script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>

    <title>导出库存记录</title>
    
    
</head>
<body style="width: 650px; height: 400px; vertical-align: middle;">
    <form id="form1" runat="server">
    <iframe id="download" name="download" height="0px" width="0px"></iframe>
    
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>

    <script language="javascript" type="text/javascript">
    function closewin() {
        window.close();
       }

     function DownloadFile() {
        var val = "SM_WarehouseStorage_Export.aspx?file=Files";
        
//        alert(val);
        var dn = new AjaxDownload(val);
        dn.EnableTrace(true);
        //fires before download, 
        dn.add_onBeginDownload(BeginDownload);
        dn.add_onEndDownload(EndDownload);
        dn.add_onError(DownloadError);
        dn.Download();
        return true;
    }
     function BeginDownload() {
        $.blockUI(); 
    }
    
    function EndDownload() {
        $.unblockUI();
    }
    
    
    function DownloadError() {
        var errMsg = AjaxDownload.ErrorMessage;
//        var errCk = $.cookie('downloaderror');
//        
//        if (errCk) {
//            errMsg += ", Error from server = " + errCk;
//        }
        alert(errMsg);
    }
    
    
    

    </script>

    <div align="center">
        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
        <table width="90%">
            <tr>
                <td width="20%" align="right" height="30px">
                    汇总方式：
                </td>
                <td width="80%" align="left">
                    <asp:RadioButtonList ID="CheckBoxListStyle" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">物料明细</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">物料汇总</asp:ListItem>
                        <asp:ListItem Value="2">钢材长宽汇总</asp:ListItem>
                        <asp:ListItem Value="3">物料名称汇总</asp:ListItem>
                        
                    </asp:RadioButtonList>
                </td>
            </tr>
           <%-- <tr>
                <td align="right" width="40%" height="30px">
                    仓库：
                </td>
                <td width="60%" align="left">
                    <asp:DropDownList ID="WarehouseDropDownList" runat="server" >
                    </asp:DropDownList>
                     <asp:DropDownList ID="ChildWarehouseDropDownList" runat="server" Visible="false">
                    <asp:ListItem Value="0">--请选择--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
           
            <tr>
                <td align="right" width="40%" height="30px">
                    物料代码：
                </td>
                <td width="60%" align="left">
                    <asp:TextBox ID="TextBoxMarID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%" height="30px">
                    物料名称：
                </td>
                <td align="left" width="60%">
                    <asp:TextBox ID="TextBoxMarNM" runat="server"></asp:TextBox>
                </td>
            </tr>--%>
        </table>
        <div align="center">
            <asp:Button ID="Comfirm" runat="server" Text="导出" OnClick="Confirm_Click" OnClientClick="DownloadFile();"/>&nbsp;&nbsp;&nbsp;
            <input id="Cancel" type="button" value="关闭" onclick="closewin()" />&nbsp;&nbsp;&nbsp;</div>
        <%-- 
    </ContentTemplate>
  
    </asp:UpdatePanel>--%>
      
        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
     <ProgressTemplate>
       <img src="../Assets/images/ajaxloader.gif" style="position:absolute;left:50%;top:40%;" alt="loading" />
     </ProgressTemplate>
    </asp:UpdateProgress>--%>
    </div>
    </form>
</body>
</html>
