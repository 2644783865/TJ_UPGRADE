<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseOut_Export3.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOut_Export3" %>

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
    function closewin() {
        window.close();
    }
      function DownloadFile() {
        var val = "SM_WarehouseOut_Export3.aspx?file=Files";
        
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
    
   

    function closewin() {
        window.close();
    }
    
  </script>  
    
    
<title>导出销售出库记录</title>
</head>
<body>
<form id="form1" runat="server">

 <iframe id="download" name="download" height="0px" width="0px"></iframe>


    <div align="center">
        <table class="edittable">
        <caption>导出销售出库记录</caption>
        <thead>
        <tr>
            <th>字段名称</th>
            <th>是否选择</th>            
            <%--<th>匹配条件</th>--%>
            <th>排序方式</th>
        </tr>
        </thead>
        <tbody>
        <tr>
            <td>销售出库单编号</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    <asp:ListItem Text="" Value="Code AS 销售出库单编号," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Code ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Code DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>    
         <tr>
            <td>计划跟踪号</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList24" runat="server">
                    <asp:ListItem Text="" Value="PTC AS 计划跟踪号," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox24" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList24" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="PTC ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="PTC DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>
        <tr>
            <td>物料编码</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList8" runat="server">
                    <asp:ListItem Text="" Value="MaterialCode AS 物料编码," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="MaterialCode ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="MaterialCode DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>   
        <tr>
            <td>物料名称</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList9" runat="server">
                    <asp:ListItem Text="" Value="MaterialName AS 物料名称," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="MaterialName ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="MaterialName DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
         <tr>
            <td>规格型号</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList12" runat="server">
                    <asp:ListItem Text="" Value="Standard AS 规格型号," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Standard ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Standard DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>    
        <tr>
            <td>材质</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList10" runat="server">
                    <asp:ListItem Text="" Value="Attribute AS 材质,"  Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Attribute ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Attribute DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>   
        <tr>
            <td>国标</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList11" runat="server">
                    <asp:ListItem Text="" Value="GB AS 国标,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="GB ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="GB DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
       
        <tr>
            <td>是否定尺</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList13" runat="server">
                    <asp:ListItem Text="" Value="Fixed AS 是否定尺,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Fixed ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Fixed DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
        <tr>
            <td>长</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList14" runat="server">
                    <asp:ListItem Text="" Value="Length AS 长,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Length ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Length DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
        <tr>
            <td>宽</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList15" runat="server">
                    <asp:ListItem Text="" Value="Width AS 宽,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Width ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Width DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>  
        <tr>
            <td>批号</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList16" runat="server">
                    <asp:ListItem Text="" Value="LotNumber AS 批号,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="LotNumber ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="LotNumber DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>单位</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList17" runat="server">
                    <asp:ListItem Text="" Value="Unit AS 单位," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox17" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Unit ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Unit DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>实际发料数量</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList18" runat="server">
                    <asp:ListItem Text="" Value="RealNumber AS 实际发料数量," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="RealNumber ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="RealNumber DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>实际发料张（支）数</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList19" runat="server">
                    <asp:ListItem Text="" Value="RealSupportNumber AS 实际发料张数或支数," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox19" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="RealSupportNumber ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="RealSupportNumber DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>单价</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList20" runat="server">
                    <asp:ListItem Text="" Value="UnitPrice AS 单价,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox20" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="UnitPrice ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="UnitPrice DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>金额</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList21" runat="server">
                    <asp:ListItem Text="" Value="Amount AS 金额,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox21" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Amount ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Amount DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
         <tr>
            <td>销售单价</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList25" runat="server">
                    <asp:ListItem Text="" Value="UnitCost AS 销售单价,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox25" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList25" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="UnitCost ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="UnitCost DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>销售金额</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList26" runat="server">
                    <asp:ListItem Text="" Value="Cost AS 销售金额,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList26" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Cost ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Cost DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        
        <tr>
            <td>发料仓库</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList22" runat="server">
                    <asp:ListItem Text="" Value="Warehouse AS 发料仓库," Selected="True" Enabled="false"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox22" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Warehouse ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Warehouse DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        <tr>
            <td>发料仓位</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList23" runat="server">
                    <asp:ListItem Text="" Value="Location AS 发料仓位," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox23" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Location ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Location DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr> 
        
        <tr>
            <td>购货单位</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                    <asp:ListItem Text="" Value="Company AS 购货单位," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Company ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Company DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>   
             
        <tr>
            <td>销售业务类型</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                    <asp:ListItem Text="" Value="case when ProcessType=0 then '销售出库'  else '受托出库' end AS 销售业务类型," ></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="ProcessType ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="ProcessType DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>              
        <tr>
            <td>制单人</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                    <asp:ListItem Text="" Value="Doc AS 制单人,"  Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Doc ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Doc DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>
        <tr>
            <td>制单日期</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                    <asp:ListItem Text="" Value="Date AS 制单日期," Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Date ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Date DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>          
        <tr>
            <td>审核人</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                    <asp:ListItem Text="" Value="Verifier AS 审核人,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="Verifier ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="Verifier DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>        
        <tr>
            <td>审核日期</td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList7" runat="server">
                    <asp:ListItem Text="" Value="left(VerifierDate,10) AS 审核日期,"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <%--<td><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>--%>
            <td>
                <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="升序" Value="VerifierDate ASC,"></asp:ListItem>
                    <asp:ListItem Text="降序" Value="VerifierDate DESC,"></asp:ListItem>
                </asp:RadioButtonList>
            </td>            
        </tr>          
        </tbody>                       
        <tfoot align="center">
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="Comfirm" runat="server" Text="确定" OnClick="Confirm_Click" OnClientClick="DownloadFile();" CssClass="button" />&nbsp;&nbsp;&nbsp;
                <input id="Cancel" type="button" value="关闭" onclick="closewin()" class="button" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        </tfoot>
    </table>
    </div>
    </form>
</body>
</html>
