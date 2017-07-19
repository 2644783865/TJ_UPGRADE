<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_ProjTemp_Export.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_ProjTemp_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <base target="download" />

    <script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


     function DownloadFile() {
        var val = "SM_Warehouse_ProjTemp_Export.aspx?file=Files";
        
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

    <title>项目结转导出</title>
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
                    单据编号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem Text="" Value="PT_CODE AS 单据编号," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PT_CODE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PT_CODE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
              <tr>
                <td>
                    生产制号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList24" runat="server">
                        <asp:ListItem Text="" Value="sczh AS 生产制号," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList24" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="sczh ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="sczh DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    项目名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList10" runat="server">
                        <asp:ListItem Text="" Value="pjname AS 项目名称," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="pjname ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="pjname DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td>
                    工程名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList25" runat="server">
                        <asp:ListItem Text="" Value="engname AS 工程名称," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList25" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="engname ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="engname DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>      
              <tr>
                <td>
                    到计划跟踪号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                        <asp:ListItem Text="" Value="PTCTo AS 到计划跟踪号," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PTCTo ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PTCTo DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td>
                   调整数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                        <asp:ListItem Text="" Value="cast(TNUM as float) AS 调整数量," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="TNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="TNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    调整张(支)数
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                        <asp:ListItem Text="" Value="cast(TFNUM as float) AS 调整张或支数," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="TFNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="TFNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    剩余原因
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList29" runat="server">
                        <asp:ListItem Text="" Value="shengyunote AS 剩余原因," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList29" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="shengyunote ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="shengyunote DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             
            <tr>
                <td>
                    从计划跟踪号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                        <asp:ListItem Text="" Value="PTCFrom AS 从计划跟踪号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PTCFrom ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PTCFrom DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料编码
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                        <asp:ListItem Text="" Value="MaterialCode AS 物料编码," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="MaterialCode ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="MaterialCode DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList7" runat="server">
                        <asp:ListItem Text="" Value="MaterialName AS 物料名称," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="MaterialName ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="MaterialName DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    规格型号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList8" runat="server">
                        <asp:ListItem Text="" Value="Standard AS 规格型号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Standard ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Standard DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    材质
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList9" runat="server">
                        <asp:ListItem Text="" Value="Attribute AS 材质," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Attribute ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Attribute DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
              <tr>
                    <td>
                        国标
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList26" runat="server">
                            <asp:ListItem Text="" Value="GB AS 国标," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList26" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
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
                        <asp:ListItem Text="" Value="Fixed AS 是否定尺,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Fixed ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Fixed DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    长
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList12" runat="server">
                        <asp:ListItem Text="" Value="Length AS 长," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Length ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Length DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    宽
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList13" runat="server">
                        <asp:ListItem Text="" Value="Width AS 宽," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Width ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Width DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    批号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList14" runat="server">
                        <asp:ListItem Text="" Value="LotNumber AS 批号,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="LotNumber ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="LotNumber DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    单位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList15" runat="server">
                        <asp:ListItem Text="" Value="Unit AS 单位," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Unit ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Unit DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                   可调数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList16" runat="server">
                        <asp:ListItem Text="" Value="cast(NUM as float) AS 可调数量," ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="NUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="NUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    可调整张（支）数
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList17" runat="server">
                        <asp:ListItem Text="" Value="cast(FNUM as float) AS 可调整张或支数,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="FNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="FNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>          
            
            <tr>
                <td>
                   仓库
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList18" runat="server">
                        <asp:ListItem Text="" Value="Warehouse AS 仓库," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Warehouse ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Warehouse DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    仓位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList19" runat="server">
                        <asp:ListItem Text="" Value="Location AS 仓位," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="Location ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="Location DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    制单人
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList20" runat="server">
                        <asp:ListItem Text="" Value="docname AS 制单人," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="docname ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="docname DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td>
                    制单日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList21" runat="server">
                        <asp:ListItem Text="" Value="PT_DATE AS 计划模式," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PT_DATE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PT_DATE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
           
             <tr>
                <td>
                    技术员
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList22" runat="server" >
                        <asp:ListItem Text="" Value="verifername AS 技术员,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="verifername ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="verifername DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    技术员审核日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList23" runat="server">
                        <asp:ListItem Text="" Value="left(PT_VERIFYDATE,10) AS 技术员审核日期," ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PT_VERIFYDATE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PT_VERIFYDATE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    审批日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList27" runat="server">
                        <asp:ListItem Text="" Value="left(PT_MANAGERTIME,10) AS 审批日期," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList27" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PT_MANAGERTIME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PT_MANAGERTIME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    审批意见
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList28" runat="server">
                        <asp:ListItem Text="" Value="PT_MANAGERNOTE AS 审批意见," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList28" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PT_MANAGERNOTE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PT_MANAGERNOTE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
