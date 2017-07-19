<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Xie_Audit.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                            <asp:Button ID="btn_add" runat="server" Text="追加" OnClick="btn_add_Click" Visible="false" />
                            <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_OnClick" />
                              <asp:Button ID="btn_xiacha" runat="server"  Text="下查" OnClick="btn_xiacha_OnClick" />
                                <asp:Button ID="btn_save" runat="server" Text="保存" onclick="btn_save_Click" />
                                <asp:Button ID="btn_dele" runat="server" Visible="false" Text="删除" OnClick="btn_dele_Click" />
                                <asp:Label ID="lbl_spzt" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Button ID="btn_Audit" runat="server" Text="提交审批" OnClick="btn_Audit_Click" OnClientClick="javascript:return confirm('确认提交吗？');" />
                                &nbsp;
                                <asp:HyperLink ID="hyl_back" runat="server" CssClass="hand" onclick="history.go(-1);">返回上一页</asp:HyperLink>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:TabContainer ID="tab_Add_caigou" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="1">
            <asp:TabPanel ID="Tab_audit" runat="server" HeaderText="审 核" TabIndex="0">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div style="height: 6px" class="box_top">
                        </div>
                        <div class="box-outer">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_info" runat="server" Text="* 所有审核通过后将自动下推，请仔细检查！" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rblSHDJ" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblSHDJ_Changed">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        生产外协申请
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="box-outer">
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Panel ID="pal_first" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect1"
                                                            PopupControlID="pal_select1">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select1" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select1_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr1" runat="server" Text="确 定" OnClick="btn_shr1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_first" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_second" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender2" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect2"
                                                            PopupControlID="pal_select2">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select2" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select2_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr2" runat="server" Text="确 定" OnClick="btn_shr2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_second" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="second_opinion" runat="server" Height="42px" TextMode="MultiLine"
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pal_third" runat="server">
                                    <tr>
                                        <td>
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                        <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                        <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand">
                                                            <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                                align="absmiddle" runat="server" />
                                                            选择
                                                        </asp:HyperLink>
                                                        <asp:PopupControlExtender ID="PopupControlExtender3" CacheDynamicResults="false"
                                                            Position="Bottom" Enabled="true" runat="server" OffsetY="8" TargetControlID="hlSelect3"
                                                            PopupControlID="pal_select3">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pal_select3" Width="340px" runat="server" Style="display: none; visibility: hidden;">
                                                            <table style='background-color: #f3f3f3; border: #A8B7EC 3px solid; font-size: 10pt;
                                                                font-family: Verdana;' cellspacing='0' cellpadding='3'>
                                                                <tr>
                                                                    <td style='background-color: #A8B7EC; color: white;'>
                                                                        <b>选择审核人</b>
                                                                    </td>
                                                                    <td style='background-color: #A8B7EC; color: white;' align='right' valign='middle'>
                                                                        <a onclick='document.body.click(); return false;' style='cursor: pointer; color: #FFFFFF;
                                                                            text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Panel ID="pal_select3_inner" runat="server">
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:Button ID="btn_shr3" runat="server" Text="确 定" OnClick="btn_shr3_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rbl_third" RepeatColumns="2" runat="server" Height="20px">
                                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核时间
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                            Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab_cgd" runat="server" HeaderText="申请单" TabIndex="1">
                <ContentTemplate>
                    <div class="box-wrapper">
                        <div class="box-wrapper">
                            <%-- <div style="height: 6px" class="box_top">
                            </div>--%>
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: x-large; text-align: center;" colspan="4">
                                            生产外协申请单
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;号：
                                            <asp:TextBox ID="tb_doc" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                        </td>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;编制日期：
                                            <asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false" Width="150px"></asp:TextBox>
                                        </td>
                                        <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;号：
                                            <asp:TextBox ID="tb_pid" runat="server" Text="" Enabled="false" Width="200px" ></asp:TextBox>
                                            <%--<asp:Label ID="tb_pid" runat="server" Text="" ></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 33%;" align="left">
                                           &nbsp;&nbsp;&nbsp; 任&nbsp;务&nbsp;号：
                                           <asp:TextBox ID="tb_engid" runat="server" Enabled="false" Text=""></asp:TextBox>
                                            <asp:TextBox ID="tb_pj" runat="server" Text="" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        </td>
                                        <td style="width: 33%;" align="left">
                                            &nbsp;&nbsp;&nbsp;设备名称：
                                            <asp:TextBox ID="tb_eng" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            交货日期：
                                            <asp:TextBox ID="tb_jhq" runat="server" class="easyui-datebox" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div style="border: 1px solid #000000;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div style="border: 1px solid #000000; height: 330px;overflow:auto">
                                                <div class="cpbox5 xscroll">
                                                    <table id="tab" class="nowrap cptable fullwidth">
                                                        <asp:Repeater ID="tbpc_otherpurbill_lookRepeater" runat="server">
                                                            <HeaderTemplate>
                                                                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                                    <td>
                                                                        <strong>行号</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>图号</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>总序</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>名称</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>规格</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>材质</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>外协数量</strong>
                                                                    </td>
<%--                                                                    <td>
                                                                    <strong>总数量</strong>
                                                                    </td>--%>
                                                                    <td>
                                                                        <strong>图纸单重(kg)</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>图纸总重(kg)</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>材料类别</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>长度</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>宽度</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>下料备注</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>下料</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>工艺流程</strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong>相关备注</strong>
                                                                    </td>
                                                                   <td>
                                                                     <strong>总备注</strong>
                                                                   </td>
                                                                    <td>
                                                                        <strong>外协类别</strong>
                                                                    </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                        <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server" Checked="false"
                                                                            onclick="checkme(this)"></asp:CheckBox>
                                                                         <asp:Label ID="lb_zj" runat="server" Text='<%#Eval("MS_ID")%>' Visible="false">
                                                                         </asp:Label>   
                                                                            <asp:Label ID="lb_id" runat="server" Text='<%#Eval("ID")%>' Visible="false">
                                                                         </asp:Label>  
                                                                           <asp:Label ID="MS_CODE" Visible="false" runat="server" Text='<%#Eval("MS_CODE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_TUHAO" runat="server" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_ZONGXU" runat="server" Text='<%#Eval("MS_ZONGXU")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_NAME" runat="server" Text='<%#Eval("MS_NAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_GUIGE" runat="server" Text='<%#Eval("MS_GUIGE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_CAIZHI" runat="server" Text='<%#Eval("MS_CAIZHI")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        
                                                                        <asp:TextBox ID="MS_NUM" runat="server" Text='<%#Eval("MS_NUM")%>'></asp:TextBox>
                                                                        <asp:Label ID="lab_num" runat="server" Visible="false" Text='<%#Eval("MS_NUM")%>' ></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_TUWGHT" runat="server" Text='<%#Eval("MS_TUWGHT")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_TUTOTALWGHT" runat="server" Text='<%#Eval("MS_TUTOTALWGHT")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_MASHAPE" runat="server" Text='<%#Eval("MS_MASHAPE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_LEN" runat="server" Text='<%#Eval("MS_LEN")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_WIDTH" runat="server" Text='<%#Eval("MS_WIDTH")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_NOTE" runat="server" Text='<%#Eval("MS_NOTE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_XIALIAO" runat="server" Text='<%#Eval("MS_XIALIAO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="MS_BZ" runat="server" BorderStyle="None" Text='<%#Eval("MS_XHBZ")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    <asp:Label ID="MS_ALLBEIZHU" runat="server" Text='<%#Eval("MS_ALLBEIZHU")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="MS_wxtype" runat="server" Text='<%#Eval("MS_wxtype")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td colspan="17" align="center">
                                                                <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                    没有数据！</asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                负责人:<asp:TextBox ID="Tb_fuziren" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="Tb_fuzirenid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                申请人:<asp:TextBox ID="Tb_shenqingren" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="Tb_shenqingrenid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                制单人:<asp:TextBox ID="tb_executor" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="tb_executorid" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
