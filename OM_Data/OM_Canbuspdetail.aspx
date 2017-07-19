<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_Canbuspdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Canbuspdetail"
    Title="餐补数据审批" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    餐补数据审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
  
    
    
    
        var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        
        function SelPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelectPerson3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
        
   
        
        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="width: 100%">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnSave" Text="提交" OnClick="btnSave_OnClick" Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnfanshen" Text="反审" OnClick="btnfanshen_OnClick"
                        Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="餐补统计明细" TabIndex="0">
            <ContentTemplate>
                <div style="height: 6px" class="box_top">
                </div>
                <div class="box-outer">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button runat="server" ID="btndelete" Text="删除" OnClick="btndelete_OnClick" Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="overflow: scroll; height: 500px;">
                    <table id="table1" align="center" cellpadding="2" cellspacing="1" border="1">
                        <tr align="center">
                            <td align="center" colspan="13" style="border: none">
                                餐补统计<asp:Label ID="lb_title" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="left" colspan="13" style="border: none">
                                制单人：<asp:Label ID="lbtitle_zdr" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单时间：<asp:Label
                                    ID="lbtitle_zdsj" runat="server"></asp:Label>
                            </td>
                        </tr>
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
                                        <asp:Label ID="lbCB_YearMonth" runat="server" Text='<%#Eval("CB_YearMonth")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbST_WORKNO" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbKQ_CHUQIN" runat="server" Text='<%#Eval("KQ_CHUQIN")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbKQ_CBTS" runat="server" Text='<%#Eval("KQ_CBTS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbcbtzts" runat="server" Width="50px" Text='<%#Eval("CB_TZTS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCB_BIAOZ" runat="server" Text='<%#Eval("CB_BIAOZ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCB_MonthCB" runat="server" Text='<%#Eval("CB_MonthCB")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCB_BuShangYue" runat="server" Text='<%#Eval("CB_BuShangYue")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCB_HeJi" runat="server" Text='<%#Eval("CB_HeJi")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCB_BeiZhu" runat="server" Text='<%#Eval("CB_BeiZhu")%>'></asp:Label>
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
                                    <td>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData1" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                        没有记录!<br />
                        <br />
                    </asp:Panel>
                </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" TabIndex="2" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:RadioButtonList runat="server" ID="rblSPJB" Enabled="false" RepeatDirection="Horizontal" RepeatColumns="3"
                                        AutoPostBack="true" OnTextChanged="rblSPJB_onchange">
                                        <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审批" Value="3" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    餐补统计
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    制单人
                                </td>
                                <td>
                                    <asp:Label ID="lbzdr" runat="server" Width="100%"></asp:Label>
                                </td>
                                <td align="center">
                                    制单时间
                                </td>
                                <td>
                                    <asp:Label ID="lbzdtime" runat="server" Width="40%" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    制单人意见
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="tbfqryj" runat="server" Height="42px" TextMode="MultiLine" Width="100%"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="yjshh" runat="server">
                                <td align="center">
                                    一级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" Visible="false" runat="server" CssClass="hand" onClick="SelPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server"
                                                    Height="20px">
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
                                                <asp:TextBox ID="opinion1" runat="server" Enabled="false" Height="42px" TextMode="MultiLine"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="ejshh" runat="server">
                                <td align="center">
                                    二级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" Visible="false" CssClass="hand" onClick="SelPersons2()">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Enabled="false"
                                                    Height="20px">
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
                                                <asp:TextBox ID="opinion2" runat="server" Enabled="false" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="sjshh" runat="server">
                                <td align="center">
                                    三级审批
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" Visible="false" CssClass="hand" onClick="SelectPerson3()">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" Enabled="false" ID="rblResult3" RepeatColumns="2">
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
                                                <asp:TextBox ID="third_opinion" Enabled="false" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
