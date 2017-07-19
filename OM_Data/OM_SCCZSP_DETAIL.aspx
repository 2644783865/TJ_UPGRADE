<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_SCCZSP_DETAIL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SCCZSP_DETAIL"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产操作人员岗位绩效审批
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
        function SelPersons3() {
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
            else if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnSave" Text="提交" OnClick="btnSave_OnClick" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnfanshen" Text="反审" OnClick="btnfanshen_OnClick"
                        Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btn_plexport" Text="导出" OnClick="btn_plexport_OnClick" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="辅助班组" TabIndex="0">
            <ContentTemplate>
                <div style="overflow: scroll; height: 500px;">
                    <table id="table1" align="center" cellpadding="2" cellspacing="1" border="1">
                        <asp:Repeater ID="rptProNumCost1" runat="server" OnItemDataBound="rptProNumCost_itemdatabind1">
                            <HeaderTemplate>
                                <tr align="center" style="background-color: #B9D3EE; text-overflow: ellipsis; white-space: nowrap;">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        编号
                                    </td>
                                    <td>
                                        年月份
                                    </td>
                                    <td>
                                        姓名
                                    </td>
                                    <td>
                                        部门
                                    </td>
                                    <td>
                                        班组
                                    </td>
                                    <td>
                                        出勤天数
                                    </td>
                                    <td>
                                        病假
                                    </td>
                                    <td>
                                        事假
                                    </td>
                                    <td>
                                        旷工
                                    </td>
                                    <td>
                                        倒休
                                    </td>
                                    <td>
                                        产假
                                    </td>
                                    <td>
                                        陪产假
                                    </td>
                                    <td>
                                        婚假
                                    </td>
                                    <td>
                                        丧假
                                    </td>
                                    <td>
                                        工伤
                                    </td>
                                    <td>
                                        年假
                                    </td>
                                    <td>
                                        周加
                                    </td>
                                    <td>
                                        节加
                                    </td>
                                    <td>
                                        延时
                                    </td>
                                    <td>
                                        绩效工资
                                    </td>
                                    <td>
                                        岗位工资
                                    </td>
                                    <td>
                                        其他（增减）
                                    </td>
                                    <td>
                                        加班工资
                                    </td>
                                    <td>
                                        合计
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                    onclick="javascript:change(this);" ondblclick="javascript:changeback(this);"
                                    style="text-overflow: ellipsis; white-space: nowrap;">
                                    <td>
                                        <asp:Label ID="lb_ID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                            Onclick="checkme(this)" />
                                        <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_BH" runat="server" Text='<%#Eval("FZBZ_BH")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_YEARMONTH" runat="server" Text='<%#Eval("FZBZ_YEARMONTH")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_ST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbDEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbST_DEPID1" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_CHUQIN" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_CHUQIN")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_BINGJ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_BINGJ")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_SHIJ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_SHIJ")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_KUANGG" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_KUANGG")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_DAOXIU" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_DAOXIU")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_CHANJIA" runat="server" align="center" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("KQ_CHANJIA")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_PEICHAN" runat="server" align="center" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("KQ_PEICHAN")%>' Width="45px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_HUNJIA" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_HUNJIA")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_SANGJIA" runat="server" align="center" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("KQ_SANGJIA")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_GONGS" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_GONGS")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_NIANX" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_NIANX")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_ZMJBAN" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_ZMJBAN")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_JRJIAB" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_JRJIAB")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_KQ_YSGZ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("KQ_YSGZ")%>' Width="30px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_FZBZ_JXGZ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("FZBZ_JXGZ")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_FZBZ_GWGZ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("FZBZ_GWGZ")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_FZBZ_QT" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("FZBZ_QT")%>' Width="50px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_FZBZ_JBGZ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("FZBZ_JBGZ")%>' Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_FZBZ_XJ" runat="server" align="center" BorderStyle="None" BackColor="Transparent"
                                            Text='<%#Eval("FZBZ_XJ")%>' Width="70px"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="18" align="right">
                                        合计:
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_JXGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_GWGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_QThj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_JBGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_FZBZ_XJhj" runat="server"></asp:Label>
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
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="一线拨付" TabIndex="1">
            <ContentTemplate>
                <div style="overflow: scroll; height: 500px;">
                    <table id="table2" align="center" cellpadding="2" cellspacing="1" border="1" width="100%">
                        <asp:Repeater ID="rptProNumCost2" runat="server" OnItemDataBound="rptProNumCost_itemdatabind2">
                            <HeaderTemplate>
                                <tr align="center" style="background-color: #B9D3EE; text-overflow: ellipsis; white-space: nowrap;">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        编号
                                    </td>
                                    <td>
                                        年月份
                                    </td>
                                    <td>
                                        姓名
                                    </td>
                                    <td>
                                        部门
                                    </td>
                                    <td>
                                        班组
                                    </td>
                                    <td>
                                        绩效工资
                                    </td>
                                    <td>
                                        岗位工资
                                    </td>
                                    <td>
                                        其他
                                    </td>
                                    <td>
                                        合计
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                    onclick="javascript:change(this);" ondblclick="javascript:changeback(this);"
                                    style="text-overflow: ellipsis; white-space: nowrap;">
                                    <td>
                                        <asp:Label ID="lb_ID1" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                            Onclick="checkme(this)" />
                                        <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YIXIAN_BH" runat="server" Text='<%#Eval("YIXIAN_BH")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YIXIAN_YEARMONTH" runat="server" Text='<%#Eval("YIXIAN_YEARMONTH")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_ST_NAME1" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbDEP_NAME1" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbST_DEPID11" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_YIXIAN_JXGZ" runat="server" align="center" Width="70px" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("YIXIAN_JXGZ")%>'></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_YIXIAN_GWGZ" runat="server" align="center" Width="70px" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("YIXIAN_GWGZ")%>'></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_YIXIAN_QT" runat="server" align="center" Width="70px" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("YIXIAN_QT")%>'></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="tb_YIXIAN_XJ" runat="server" align="center" Width="70px" BorderStyle="None"
                                            BackColor="Transparent" Text='<%#Eval("YIXIAN_XJ")%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="6" align="right">
                                        合计:
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YIXIAN_JXGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YIXIAN_GWGZhj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YIXIAN_QThj" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lb_YIXIAN_XJhj" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="palNoData2" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                        没有记录!<br />
                        <br />
                    </asp:Panel>
                </div>
                <uc1:UCPaging ID="UCPaging2" runat="server" />
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel3" TabIndex="2" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblSHJS_OnSelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    班组绩效岗位工资
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
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px">
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
                                                <asp:TextBox ID="opinion1" runat="server" Height="42px" TextMode="MultiLine" Width="100%"></asp:TextBox>
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
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelPersons2()">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px">
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
                                                <asp:TextBox ID="opinion2" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="sjshh" runat="server">
                                <td align="center">
                                    三级审核
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelPersons3()">
                                                    <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px">
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
                                                <asp:TextBox ID="opinion3" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
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
