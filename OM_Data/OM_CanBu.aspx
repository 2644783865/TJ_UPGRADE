<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="OM_CanBu.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CanBu" Title="餐补统计" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    餐补统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script language="javascript" type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#tab input[type=checkbox]").attr("checked","true");
}
else{
 $("#tab input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；

    function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        
            function rebuild_cbsj(){
        var button_txt_dou=document.getElementById("<%=btncreatdata.ClientID%>");
        var button_txt_dou_value=button_txt_dou.value;
        var t_f_val=true;
        if(button_txt_dou_value=="重新生成本月数据"){
            t_f_val=confirm("重新生成本月数据后，餐补异动数据将清零，是否确认进行重新生成本月数据");
        }
        return t_f_val;
    }
    </script>


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="20">
    </asp:ToolkitScriptManager>
    <div class="box_right">
        <div class="box-inner">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;
                        <strong>时间：</strong>
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;
                        &nbsp;&nbsp;&nbsp; 
                        <strong>部门：</strong>&nbsp;
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                            OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp; <strong>姓名：</strong>&nbsp;
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        &nbsp;&nbsp; <strong>餐补标准：</strong>&nbsp;
                        <asp:TextBox ID="tbcbbz" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;
                        <input id="Checkbox2" type="checkbox" />全选&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btncreatdata" runat="server" Text="生成餐补数据" OnClick="btncreatdata_OnClick" OnClientClick="return rebuild_cbsj()"  />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_OnClick" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnplxgclient" runat="server" Text="批量修改餐补标准" OnClientClick="viewCondition()" />
                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnplxgclient"
                        PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                        Y="90" X="775">
                        </asp:ModalPopupExtender>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_plexport" Text="导出" OnClick="btn_plexport_OnClick" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hlAdd" NavigateUrl="~/OM_DaTa/OM_CanBuAdd.aspx?action=add"
                            runat="server">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" BorderWidth="0"
                                hsapce="2" ImageAlign="AbsMiddle" runat="server" />添加
                        </asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hplsushe" runat="server" NavigateUrl="~/OM_Data/OM_CanBuBMHZ.aspx"
                            Target="_blank" Font-Underline="false">
                            <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                    runat="server" />餐补按部门汇总</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            
             <asp:Panel ID="PanelCondition" runat="server" Width="150px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button ID="btnplxg" runat="server" OnClick="btnplxg_OnClick" Text="确认" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="取消" />
                    </td>
                </tr>
                <tr>
                    <td>
                        新餐补标准：<asp:TextBox ID="tbcbbzxg" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </div>
    </div>
    <div class="box_right">
        <div class="box-wrapper">
            <div style="width: 100%; height: auto; overflow: scroll; display: block">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                    <asp:Repeater ID="rptCanBu" runat="server" OnItemDataBound="rptCanBu_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    年月份
                                </td>
                                <td>
                                    工号
                                </td>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    部门
                                </td>
                                <td>
                                    出勤
                                </td>
                                <td>
                                    餐补天数
                                </td>
                                <td>
                                    调整天数
                                </td>
                                <td>
                                    餐补标准
                                </td>
                                <td>
                                    月度餐补
                                </td>
                                <td>
                                    补发
                                </td>
                                <td>
                                    合计
                                </td>
                                <td>
                                    备注
                                </td>
                                <td>
                                    修改
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lbCB_ID" Visible="false" Text='<%#Eval("CB_ID")%>'></asp:Label>
                                    <asp:CheckBox ID="cbxNumber" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_YearMonth" runat="server" Width="50px" Text='<%#Eval("CB_YearMonth")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbST_WORKNO" runat="server" Width="50px" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbST_NAME" runat="server" Width="50px" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                    <asp:Label ID="lbzzZT" runat="server" Text='<%#Eval("zzZT")%>' Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbDEP_NAME" runat="server" Width="50px" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbKQ_CHUQIN" runat="server" Width="50px" Text='<%#Eval("KQ_CHUQIN")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbKQ_CBTS" runat="server" Width="50px" Text='<%#Eval("KQ_CBTS")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbcbtzts" runat="server" Width="50px" Text='<%#Eval("CB_TZTS")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_BIAOZ" runat="server" Width="50px" Text='<%#Eval("CB_BIAOZ")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_MonthCB" runat="server" Width="50px" Text='<%#Eval("CB_MonthCB")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_BuShangYue" runat="server" Width="50px" Text='<%#Eval("CB_BuShangYue")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_HeJi" runat="server" Width="50px" Text='<%#Eval("CB_HeJi")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbCB_BeiZhu" runat="server" Width="50px" Text='<%#Eval("CB_BeiZhu")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlAlter" runat="server" NavigateUrl='<%#"OM_CanBuAdd.aspx?action=Alter&id="+Eval("CB_ID")%>'>
                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        修改
                                    </asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="9" align="right">
                                    合计
                                </td>
                                <td>
                                    <asp:Label ID="lb_ydcb" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lb_bf" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lb_cbzj" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNodata" runat="server" ForeColor="Red" Visible="false" HorizontalAlign="Center">
                    <br />
                    没有记录<br />
                </asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
