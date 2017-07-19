<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_ProjOver_Export.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_ProjOver_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <base target="download" />

    <script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


     function DownloadFile() {
        var val = "SM_Warehouse_ProjOver_Export.aspx?file=Files";
        
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

    <title>导出完工库存</title>
</head>
<body>
    <form id="form1" runat="server">
    <iframe id="download" name="download" height="0px" width="0px"></iframe>
    <div align="center">
        <div>
            <asp:Button ID="Comfirm" runat="server" Text="确定" OnClick="Confirm_Click" OnClientClick="DownloadFile();" />&nbsp;&nbsp;&nbsp;
            <input id="Cancel" type="button" value="关闭" onclick="closewin()" />&nbsp;&nbsp;&nbsp;</div>
        <table>
            <tr>
                <th>
                    字段名称
                </th>
                <th>
                    是否选择
                </th>
                <th>
                    匹配条件
                </th>
                <th>
                    排序方式
                </th>
            </tr>
            <tr>
                <td>
                    生产制号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem Text="" Value="SQ_TASKID AS 生产制号," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_TASKID ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_TASKID DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
              <tr>
                <td>
                    项目名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                        <asp:ListItem Text="" Value="SQ_PRJ AS 项目名称," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_PRJ ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_PRJ DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
              <tr>
                <td>
                    工程名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                        <asp:ListItem Text="" Value="SQ_ENG AS 工程名称," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_ENG ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_ENG DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
              <tr>
                <td>
                    确认时间
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                        <asp:ListItem Text="" Value="SQ_CONFIRMTIME AS 确认时间," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_CONFIRMTIME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_CONFIRMTIME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    计划跟踪号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                        <asp:ListItem Text="" Value="SQ_PTC AS 计划跟踪号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_PTC ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_PTC DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料编码
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                        <asp:ListItem Text="" Value="SQ_MARID AS 物料编码," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_MARID ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_MARID DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList7" runat="server">
                        <asp:ListItem Text="" Value="MNAME AS 物料名称," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="MNAME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="MNAME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    规格型号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList8" runat="server">
                        <asp:ListItem Text="" Value="GUIGE AS 规格型号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="GUIGE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="GUIGE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    材质
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList9" runat="server">
                        <asp:ListItem Text="" Value="CAIZHI AS 材质," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="CAIZHI ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="CAIZHI DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    国标
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList10" runat="server">
                        <asp:ListItem Text="" Value="GB AS 国标," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="GB ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="GB DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    是否定尺
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList11" runat="server">
                        <asp:ListItem Text="" Value="SQ_FIXED AS 是否定尺,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_FIXED ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_FIXED DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    长
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList12" runat="server">
                        <asp:ListItem Text="" Value="SQ_LENGTH AS 长," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_LENGTH ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_LENGTH DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    宽
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList13" runat="server">
                        <asp:ListItem Text="" Value="SQ_WIDTH AS 宽," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_WIDTH ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_WIDTH DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    批号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList14" runat="server">
                        <asp:ListItem Text="" Value="SQ_LOTNUM AS 批号,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_LOTNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_LOTNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    单位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList15" runat="server">
                        <asp:ListItem Text="" Value="PURCUNIT AS 单位," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PURCUNIT ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PURCUNIT DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList16" runat="server">
                        <asp:ListItem Text="" Value="cast(SQ_NUM as float) AS 数量," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_NUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_NUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    辅助数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList17" runat="server">
                        <asp:ListItem Text="" Value="cast(SQ_FZNUM as float) AS 辅助数量," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_FZNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_FZNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
           
            
            <tr>
                <td>
                   仓库
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList18" runat="server">
                        <asp:ListItem Text="" Value="WS_NAME AS 仓库," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WS_NAME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WS_NAME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    仓位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList19" runat="server">
                        <asp:ListItem Text="" Value="WL_NAME AS 仓位," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WL_NAME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WL_NAME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    订单编号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList20" runat="server">
                        <asp:ListItem Text="" Value="SQ_ORDERID AS 订单编号,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_ORDERID ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_ORDERID DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td>
                    计划模式
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList21" runat="server">
                        <asp:ListItem Text="" Value="SQ_PMODE AS 计划模式,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_PMODE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_PMODE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
           
             <tr>
                <td>
                    标识号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList22" runat="server" >
                        <asp:ListItem Text="" Value="SQ_CGMODE AS 标识号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_CGMODE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_CGMODE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList23" runat="server">
                        <asp:ListItem Text="" Value="SQ_NOTE AS 备注,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SQ_NOTE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SQ_NOTE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
