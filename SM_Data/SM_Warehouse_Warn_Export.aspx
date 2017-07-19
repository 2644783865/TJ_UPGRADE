<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_Warn_Export.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_Warn_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

<base target="download"/>

<script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>
<script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>
<script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>
<script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>
<script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>
<link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />


<script type="text/javascript" language="javascript">

//    function DownloadFile() {
//        var val = "SM_WarehouseOut_Export.aspx?file=Files";
//        
////        alert(val);
//        var dn = new AjaxDownload(val);
//        dn.EnableTrace(true);
//        //fires before download, 
//        dn.add_onBeginDownload(BeginDownload);
//        dn.add_onEndDownload(EndDownload);
//        dn.add_onError(DownloadError);
//        dn.Download();
//        return true;
//    }
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

    function closewin() {
        window.close();
    }
    
</script>

    <title>导出记录</title>
</head>
<body>
    <form id="form1" runat="server">

     <iframe id="download" name="download" height="0px" width="0px"></iframe>
            
    <div align="center">
         <table class="edittable">
        <caption>导出记录</caption>
        <thead>
        <tr>
            <th>字段名称</th>
            <th>是否选择</th>            
            <th>匹配条件</th>
            <th>排序方式</th>
        </tr>
        </thead>
        <tbody>
       
           
        <tr>
            <td>物料编码</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    <asp:ListItem Text="" Value="MARID AS 物料编码," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="MARID ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="MARID DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>   
        <tr>
            <td>物料名称</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                    <asp:ListItem Text="" Value="MNAME AS 物料名称," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="MNAME ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="MNAME DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
        <tr>
            <td>规格型号</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                    <asp:ListItem Text="" Value="GUIGE AS 规格型号,"  Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="GUIGE ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="GUIGE DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>   
        <tr>
            <td>材质</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                    <asp:ListItem Text="" Value="CAIZHI AS 材质," ></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="CAIZHI ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="CAIZHI DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>   
        <tr>
            <td>国标</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                    <asp:ListItem Text="" Value="GB AS 国标,"  ></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="GB ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="GB DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
        
        <tr>
            <td>单位</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                    <asp:ListItem Text="" Value="PURCUNIT AS 单位," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="PURCUNIT ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="PURCUNIT DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
        <tr>
            <td>安全库存</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList7" runat="server" Selected="True">
                    <asp:ListItem Text="" Value="WARNNUM AS 安全库存," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="WARNNUM ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="WARNNUM DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>
         
        </tbody>                       
        <tfoot>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="Comfirm" runat="server" Text="确定" OnClick="Confirm_Click"  CssClass="button" />&nbsp;&nbsp;&nbsp;
                <input id="Cancel" type="button" value="关闭" onclick="closewin()" class="button" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        </tfoot>
    </table>       
    </div>
    </form>
</body>
</html>
