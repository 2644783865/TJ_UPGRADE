<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CB_ProNum_Cost.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CB_ProNum_Cost" %>

<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    财务成本计算
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

     <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#table1 input[type=checkbox]").attr("checked","true");
}
else{
 $("#table1 input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class=class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            
                            <td>
                                全选/取消<input id="Checkbox2" type="checkbox" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="center">
                                <strong>请输入要查询的任务号:</strong><asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <%--<td align="right">
                                <asp:Button ID="btn" runat="server" Text="导出到EXCEL" OnClick="btn_export_Click" />
                            </td>
                            <td>
                            <asp:Button ID="btn_pl" runat="server" Text="批量导出" OnClick="btn_plexport_Click" />
                            </td>--%>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="overflow: scroll;height: auto;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center">
                                <th rowspan="2">
                                    序号
                                </th>
                                <th rowspan="2">
                                    任务号
                                </th>
                                <th rowspan="2">
                                    项目名称
                                </th>
                                <th rowspan="2">
                                    设备名称
                                </th>
                                <th rowspan="2">
                                    工资
                                </th>
                                <th colspan="3">
                                    直接人工费
                                </th>
                                <th colspan="9">
                                    直接材料费
                                </th>
                                <th colspan="3">
                                    制造费用
                                </th>
                                <th rowspan="2">
                                    外协费用
                                </th>
                                <th rowspan="2">
                                    厂内分包
                                </th>
                                <th rowspan="2">
                                    运费
                                </th>
                                <th rowspan="2">
                                    分交成本
                                </th>
                                <th rowspan="2">
                                    其他
                                </th>
                            </tr>
                            <tr>
                                <th>
                                 机加费用
                                </th>
                                <th>
                                 厂内结构费用
                                </th>
                                <th>
                                 小计
                                </th>
                                
                                <th>
                                    外购件
                                </th>
                                <th>
                                    黑色金属
                                </th>
                                <th>
                                    焊材类
                                </th>
                                
                                <th>
                                    铸件
                                </th>
                                <th>
                                    锻件
                                </th>
                                <th>
                                    轴承
                                </th>
                                <th>
                                    标准件
                                </th>
                                <th>
                                    其他类
                                </th>
                                <th>
                                    材料小计
                                </th>
                                <th>
                                    固定制造费用
                                </th>
                                <th>
                                    可变制造费用
                                </th>
                                <th>
                                    制造费用小计
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="cbxSelect"
                                        runat="server" />
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("TASK_ID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbprojname" runat="server" Text='<%#Eval("PRJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbsbname" runat="server" Text='<%#Eval("ENG")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbgz" runat="server" Width="90px" Text='<%#Eval("CWCB_GZ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjjfy" runat="server" Width="90px" Text='<%#Eval("CWCB_JJFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjgyzfy" runat="server" Width="90px" Text='<%#Eval("CWCB_JGYZ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzjrgf" runat="server" Width="90px" Text='<%#Eval("ZJRGFXJ")%>'></asp:Label>
                                </td>
                                
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbwgj" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_WGJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhsjs" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_HSJS")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhcl" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_HCL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzj" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_ZJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbdj" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_DJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzc" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_ZC")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbbzj" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_BZJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqtcl" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_QTL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbclxj" runat="server" Width="90px" align="center" Text='<%#Eval("CLXJ")%>'></asp:Label>
                                </td>
                                
                                <td align="center">
                                    <asp:Label ID="lbgdzzfy" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_GDZZ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbkbzzfy" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_KBZZ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzzfyxj" runat="server" Width="90px" align="center" Text='<%#Eval("ZZFYXJ")%>'></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbwxfy" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_WXFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbcnfb" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_CNFB")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyf" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_YF")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbfjcb" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_FJCB")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqt" runat="server" Width="90px" align="center" Text='<%#Eval("CWCB_QT")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="2" align="right">
                                    合计：
                                </td>
                                <td colspan="2" align="right">
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbgzhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjjfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbjgyzfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzjrgfhj" runat="server"></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbwgjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhsjshj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhclhj" runat="server"></asp:Label>
                                </td>
                                
                                <td align="center">
                                    <asp:Label ID="lbzjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbdjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzchj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbbzjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqtclhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbclhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbgdzzfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbkbzzfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbzzfyhj" runat="server"></asp:Label>
                                </td>
                                
                                <td align="center">
                                    <asp:Label ID="lbwxfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbcnfbhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyfhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbfjcbhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqthj" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
    </div>
</asp:Content>
