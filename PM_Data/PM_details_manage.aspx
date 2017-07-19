<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="PM_details_manage.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_details_manage" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细信息表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script type="text/javascript">
        function Ibtn_look_OnClientClick(obj) {
            var lx = $("#<%=rblstate.ClientID %> input:radio:checked").val();
            var msid = $(obj).parent().find("input[name*=hidMS_ID]").val();
            if (lx == "0") {
                window.open("PM_Xie_List_Detail.aspx?action=look&mnpid=" + msid);
            }
            if (lx == "1") {
                window.open("../TM_Data/TM_MS_Detail_Audit.aspx?ms_audit_id=" + msid);
            }
        }
    </script>

    <div class="RightContent">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table style="width: 100%; height: 24px">
                        <tr>
                            <td>
                                制作明细状态：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblstate" RepeatColumns="2" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="rblstate_SelectedIndexChanged">
                                    <asp:ListItem Text="正常" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="变更" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <%--   <asp:RadioButtonList ID="rblstate1" RepeatColumns="2" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="rblstate_SelectedIndexChanged">                
                            <asp:ListItem Text="已完工" value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="未完工" Value="1" ></asp:ListItem>
                             </asp:RadioButtonList>--%>
                                <asp:RadioButton ID="rb_1" runat="server" Text="已完工" AutoPostBack="true" GroupName="select"
                                    OnCheckedChanged="rblstate_SelectedIndexChanged" />
                                <asp:RadioButton ID="rb_2" runat="server" Text="未完工" AutoPostBack="true" GroupName="select"
                                    OnCheckedChanged="rblstate_SelectedIndexChanged" />
                            </td>
                            <%-- <td  style="width: 15%; text-align:right">
                                        合同名称：
                                        <asp:DropDownList ID="ddlpjname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpjname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </td>
                                        <td  style="width:40%;text-align:left">
                                        &nbsp;&nbsp; 设备名称：<asp:DropDownList ID="ddlengname" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlengname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>--%>
                            <td>
                                <asp:CheckBox ID="cb_myjob" runat="server" Text="我的任务" AutoPostBack="true" OnCheckedChanged="cb_myjob_OnCheckedChanged" />
                            </td>
                            <td>
                                按合同号查询：<asp:TextBox runat="server" ID="txtHTH"></asp:TextBox>
                                <asp:Button runat="server" ID="btnHTH" OnClick="btnHTH_OnClick" Text="查看" />
                            </td>
                            <td>
                                <asp:Button ID="btn_fengong" runat="server" Text="任务分工" />
                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_fengong"
                                    PopupControlID="palscwx" Y="102" X="600" CancelControlID="close">
                                </asp:ModalPopupExtender>
                            </td>
                            <td>
                                <asp:Button ID="btn_finish" runat="server" Text="确定完工" OnClick="btn_finish_OnClick" />
                                <asp:Button ID="btn_look" runat="server" Text="确定变更" OnClick="btn_look_OnClick" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btn_query" runat="server" Text="筛选" />
                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btn_query"
                                    PopupControlID="select" Y="80" X="700" CancelControlID="guan">
                                </asp:ModalPopupExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnexport" runat="server" Text="导 出" OnClick="btnexport_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="palscwx" Style="display: none; border-style: solid; border-width: 1px;
        border-color: blue; background-color: White; position: absolute;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="250px">
                    <tr>
                        <td>
                            <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                <a id="close" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF;
                                    text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                            </div>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            选择分工人员：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_fengfong" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br />
                            <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click"
                                UseSubmitBehavior="false" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConfirm" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="select" Style="display: none; border-style: solid; border-width: 1px;
        border-color: blue; background-color: Menu;" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="400px;">
                    <tr>
                        <td>
                            <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                <a id="guan" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF; text-align: center;
                                    text-decoration: none; padding: 5px;" title="关闭">X</a>
                            </div>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="60px">
                            <asp:DropDownList ID="screen1" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="100px">
                            <asp:DropDownList ID="ddlRelation1" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                <asp:ListItem Value="1">不包含</asp:ListItem>
                                <asp:ListItem Value="2">等于</asp:ListItem>
                                <asp:ListItem Value="3">不等于</asp:ListItem>
                                <asp:ListItem Value="4">大于</asp:ListItem>
                                <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                <asp:ListItem Value="6">小于</asp:ListItem>
                                <asp:ListItem Value="7">小于或等于</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt1" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogic1" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="OR">或者</asp:ListItem>
                                <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="60px">
                            <asp:DropDownList ID="screen2" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="100px">
                            <asp:DropDownList ID="ddlRelation2" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                <asp:ListItem Value="1">不包含</asp:ListItem>
                                <asp:ListItem Value="2">等于</asp:ListItem>
                                <asp:ListItem Value="3">不等于</asp:ListItem>
                                <asp:ListItem Value="4">大于</asp:ListItem>
                                <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                <asp:ListItem Value="6">小于</asp:ListItem>
                                <asp:ListItem Value="7">小于或等于</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt2" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogic2" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="OR">或者</asp:ListItem>
                                <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="60px">
                            <asp:DropDownList ID="screen3" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="100px">
                            <asp:DropDownList ID="ddlRelation3" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                <asp:ListItem Value="1">不包含</asp:ListItem>
                                <asp:ListItem Value="2">等于</asp:ListItem>
                                <asp:ListItem Value="3">不等于</asp:ListItem>
                                <asp:ListItem Value="4">大于</asp:ListItem>
                                <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                <asp:ListItem Value="6">小于</asp:ListItem>
                                <asp:ListItem Value="7">小于或等于</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt3" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogic3" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="OR">或者</asp:ListItem>
                                <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="60px">
                            <asp:DropDownList ID="screen4" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="100px">
                            <asp:DropDownList ID="ddlRelation4" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                <asp:ListItem Value="1">不包含</asp:ListItem>
                                <asp:ListItem Value="2">等于</asp:ListItem>
                                <asp:ListItem Value="3">不等于</asp:ListItem>
                                <asp:ListItem Value="4">大于</asp:ListItem>
                                <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                <asp:ListItem Value="6">小于</asp:ListItem>
                                <asp:ListItem Value="7">小于或等于</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt4" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogic4" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="OR">或者</asp:ListItem>
                                <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="60px">
                            <asp:DropDownList ID="screen5" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="100px">
                            <asp:DropDownList ID="ddlRelation5" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                <asp:ListItem Value="1">不包含</asp:ListItem>
                                <asp:ListItem Value="2">等于</asp:ListItem>
                                <asp:ListItem Value="3">不等于</asp:ListItem>
                                <asp:ListItem Value="4">大于</asp:ListItem>
                                <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                <asp:ListItem Value="6">小于</asp:ListItem>
                                <asp:ListItem Value="7">小于或等于</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt5" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="OR">或者</asp:ListItem>
                                <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="60px">
                            <asp:DropDownList ID="screen6" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="100px">
                            <asp:DropDownList ID="ddlRelation6" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                <asp:ListItem Value="1">不包含</asp:ListItem>
                                <asp:ListItem Value="2">等于</asp:ListItem>
                                <asp:ListItem Value="3">不等于</asp:ListItem>
                                <asp:ListItem Value="4">大于</asp:ListItem>
                                <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                <asp:ListItem Value="6">小于</asp:ListItem>
                                <asp:ListItem Value="7">小于或等于</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt6" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogic6" BackColor="AliceBlue" runat="server">
                                <asp:ListItem Value="OR">或者</asp:ListItem>
                                <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right">
                            <asp:Button ID="reset" runat="server" Text="重 置" OnClick="reset_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnfind" runat="server" Text="搜 索" OnClick="search_Click" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnfind" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 450px; overflow: auto; width: 100%">
                <div class="cpbox2 xscroll">
                    <table id="tab" align="center" class="nowrap cptable fullwidth">
                        <asp:Repeater ID="details_repeater" runat="server" OnItemDataBound="details_repeater_databound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                        <strong>行号</strong>
                                    </td>
                                    <td>
                                        <strong>批号</strong>
                                    </td>
                                    <td>
                                        <strong>合同</strong>
                                    </td>
                                    <td>
                                        <strong>项目名称</strong>
                                    </td>
                                    <td>
                                        <strong>制图员</strong>
                                    </td>
                                    <td>
                                        <strong>任务号</strong>
                                    </td>
                                    <td>
                                        <strong>设备</strong>
                                    </td>
                                    <td>
                                        <strong>子设备名称</strong>
                                    </td>
                                    <td>
                                        <strong>图号</strong>
                                    </td>
                                    <td>
                                        <strong>下发时间</strong>
                                    </td>
                                    <td>
                                        <strong>调度员</strong>
                                    </td>
                                    <td>
                                        <strong>状态</strong>
                                    </td>
                                    <td>
                                        <strong>查看</strong>
                                    </td>
                                    <td>
                                        <strong>确认已查看</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td id="td_1" runat="server">
                                        <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                            onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ID" runat="server" Text='<%#Eval("MS_ID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_PJNAME" runat="server" Text='<%#Eval("MS_PJNAME")%>'></asp:Label>
                                    </td>
                                    <td id="td_5" runat="server">
                                        <asp:Label ID="MS_PERSON" runat="server" Text='<%#Eval("MS_PERSON")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ENGID" runat="server" Text='<%#Eval("MS_ENGID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <div style="width: 200px; white-space: normal">
                                            <asp:Label ID="MS_ENGNAME" runat="server" Text='<%#Eval("MS_ENGNAME")%>'></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_CHILDENGNAME" runat="server" Text='<%#Eval("MS_CHILDENGNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_MAP" runat="server" Text='<%#Eval("MS_MAP")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ADATE" runat="server" Text='<%#Eval("MS_ADATE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MTA_DUY" runat="server" Text='<%#Eval("MTA_DUY")%>'></asp:Label>
                                    </td>
                                    <td id="td_12" runat="server">
                                        <%--<asp:Label ID="lab_status1" runat="server" Text='<%#Eval("MS_FINISHSTATUS").ToString()=="1"?"已完工":Eval("MS_FINISHSTATUS").ToString()=="0"?"未完工":""%>'></asp:Label>--%>
                                        <asp:Label ID="lab_status1" runat="server" Text='<%#get_pur_bjdsh(Eval("MS_FINISHSTATUS").ToString()) %>'></asp:Label>
                                        <asp:Label ID="lab_status" runat="server" Text='<%#Eval("MS_FINSTATUS").ToString()=="1"?"已确定变更":Eval("MS_FINSTATUS").ToString()=="0"?"未确定变更":""%>'></asp:Label>
                                        <asp:Label ID="MS_LOOKSTATUS" runat="server" Text='<%#Eval("MS_LOOKSTATUS")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:HyperLink ID="hlDetail"  CssClass="link" runat="server" NavigateUrl='<%#"PM_Xie_List_Detail.aspx?action=look&mnpid="+Eval("MS_ID")%>' Target="_blank"  >
                                      <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server"  />
                                        <asp:ImageButton ID="Ibtn_look" ToolTip="查看" ImageUrl="~/Assets/images/create.gif" CommandArgument='<%#Eval("MS_ID")%>' ImageAlign="AbsMiddle" runat="server"  OnClick="Ibtn_look_click" />
                                    </asp:HyperLink>--%>
                                        <input id="Button1" runat="server" value="查看" type="button" style="color: Red" onclick="Ibtn_look_OnClientClick(this)" />
                                        <input runat="server" name="hidMS_ID" type="hidden" id="hidMS_ID" value='<%#Eval("MS_ID")%>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btn_Confirm_ck" runat="server" CommandArgument='<%#Eval("MS_ID")+","+Eval("MS_CK_BT") %>'
                                            Text="确定" OnClick="btn_Confirm_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="11" align="center">
                                <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                    没有记录！</asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                    <hr style="width: 100%; height: 0.1px; color: Blue;" />
                    没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
    </div>
</asp:Content>
