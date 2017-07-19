<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="tbds_gz.aspx.cs"
    Inherits="ZCZJ_DPF.OM_Data.tbds_gz" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 23%;">
                            <strong>时间：</strong>
                            <asp:DropDownList ID="dplYear" runat="server">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
                        </td>
                        <td style="width: 40%;">
                            <strong>按姓名查询：</strong><asp:TextBox ID="txtSCZH" ForeColor="Gray" runat="server"
                                onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);" Width="120px"></asp:TextBox>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            勾选隐藏列：
                            <asp:CheckBox ID="cb_cc" runat="server" AutoPostBack="true" OnCheckedChanged="cb_cc_CheckedChanged" />
                            部门 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="cb_qj" runat="server" AutoPostBack="true" OnCheckedChanged="cb_qj_CheckedChanged" />
                            考勤 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="cb_wx" runat="server" AutoPostBack="true" OnCheckedChanged="cb_wx_CheckedChanged" />
                            五险一金
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="width: 100%; height: auto; overflow: scroll; display: block;">
                            <table id="tab" align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                                <asp:Repeater ID="rptProNumCost" runat="server"                                
                                    onitemdatabound="rptProNumCost_ItemDataBound1">
                                    <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;">
                                            <td>
                                                序号
                                            </td>
                                            <td>
                                                工号
                                            </td>
                                            <td>
                                                姓名
                                            </td>
                                            <td>
                                                月份
                                            </td>
                                            <td>
                                                区分标识
                                            </td>
                                            <td runat="server" id="TD1">
                                                部门
                                            </td>
                                            <td runat="server" id="TD2">
                                                班组
                                            </td>
                                            <td runat="server" id="TD3">
                                                月出勤合计
                                            </td>
                                            <td runat="server" id="TD4">
                                                节日工作
                                            </td>
                                            <td runat="server" id="TD5">
                                                周休息工作
                                            </td>
                                            <td runat="server" id="TD6">
                                                日延时工作
                                            </td>
                                            <td runat="server" id="TD7">
                                                病假
                                            </td>
                                            <td runat="server" id="TD8">
                                                事假
                                            </td>
                                            <td runat="server" id="TD9">
                                                年假
                                            </td>
                                            <td>
                                                基础工资
                                            </td>
                                            <td>
                                                工资工龄
                                            </td>
                                            <td>
                                                固定工资
                                            </td>
                                            <td>
                                                绩效工资
                                            </td>
                                            <td>
                                                奖励
                                            </td>
                                            <td>
                                                病假工资
                                            </td>
                                            <td>
                                                加班工资
                                            </td>
                                            <td>
                                                中夜班费
                                            </td>
                                            <td>
                                                年假工资
                                            </td>
                                            <td>
                                                应扣岗位
                                            </td>
                                            <td>
                                                调整/补发
                                            </td>
                                            <td>
                                                调整/补扣
                                            </td>
                                            <td>
                                                应付合计
                                            </td>
                                            <td runat="server" id="TD10">
                                                养老保险
                                            </td>
                                            <td runat="server" id="TD11">
                                                失业保险
                                            </td>
                                            <td runat="server" id="TD12">
                                                医疗保险
                                            </td>
                                            <td runat="server" id="TD13">
                                                大额救助
                                            </td>
                                            <td runat="server" id="TD14">
                                                补保险
                                            </td>
                                            <td runat="server" id="TD15">
                                                公积金
                                            </td>
                                            <td>
                                                房租/水电费
                                            </td>
                                            <td>
                                                个税
                                            </td>
                                            <td>
                                                代扣小计
                                            </td>
                                            <td>
                                                实发金额
                                            </td>
                                            <td>
                                                签章
                                            </td>
                                            <td>
                                                岗位
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr onmouseover="this.className='highlight'" onclick="javascript:change(this);" ondblclick="javascript:changeback(this);"
                                            onmouseout="this.className='baseGadget'">
                                            <td align="center">
                                                <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="cbxSelect"
                                                    runat="server" Visible="false" Checked="true" />
                                            </td>
                                            <td id="Td1" runat="server" align="right">
                                                <asp:TextBox ID="TBGH" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("KQ_PersonID")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td2" runat="server" align="right">
                                                <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="80px" Text='<%#Eval("ST_NAME")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td3" runat="server" align="right">
                                                <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("time")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td4" runat="server" align="right">
                                                <asp:TextBox ID="TextBox4" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td1_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox5" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="90px"></asp:TextBox>
                                            </td>
                                            <td id="Td2_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox6" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox30" runat="server" Text='<%#Eval("KQ_CHUQIN")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_2" runat="server" align="right">
                                                <asp:TextBox ID="TextBox31" runat="server" Text='<%#Eval("KQ_JRJIAB")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_3" runat="server" align="right">
                                                <asp:TextBox ID="TextBox32" runat="server" Text='<%#Eval("KQ_ZMJBAN")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_4" runat="server" align="right">
                                                <asp:TextBox ID="TextBox33" runat="server" Text='<%#Eval("KQ_YSGZ")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_5" runat="server" align="right">
                                                <asp:TextBox ID="TextBox34" runat="server" Text='<%#Eval("KQ_BINGJ")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_6" runat="server" align="right">
                                                <asp:TextBox ID="TextBox35" runat="server" Text='<%#Eval("KQ_SHIJ")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td3_7" runat="server" align="right">
                                                <asp:TextBox ID="TextBox36" runat="server" Text='<%#Eval("KQ_NIANX")%>' BorderStyle="None"
                                                    BackColor="Transparent" Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td37" runat="server" align="right">
                                                <asp:TextBox ID="TextBox37" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td38" runat="server" align="right">
                                                <asp:TextBox ID="TextBox38" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px"></asp:TextBox>
                                            </td>
                                            <td id="Td7" runat="server" align="right">
                                                <asp:TextBox ID="TextBox7" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("Gwages")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td8" runat="server" align="right">
                                                <asp:TextBox ID="TextBox8" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("Jwages")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td9" runat="server" align="right">
                                                <asp:TextBox ID="TextBox9" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("JL")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td39" runat="server" align="right">
                                                <asp:TextBox ID="TextBox39" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("BJGZ")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td10" runat="server" align="right">
                                                <asp:TextBox ID="TextBox10" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("JBGZ")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td11" runat="server" align="right">
                                                <asp:TextBox ID="TB_ZYB" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("GZ_ZYB")%>' onfocus="javascript:setToolTipGet(this);"
                                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td12" runat="server" align="right">
                                                <asp:TextBox ID="TextBox12" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("NJGZ1")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td13" runat="server" align="right">
                                                <asp:TextBox ID="TextBox13" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("YKJC")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td14" runat="server" align="right">
                                                <asp:TextBox ID="TB_TZBF" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("GZ_TZBF")%>' onfocus="javascript:setToolTipGet(this);"
                                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td15" runat="server" align="right">
                                                <asp:TextBox ID="TB_TZBK" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("GZ_TZBK")%>' onfocus="javascript:setToolTipGet(this);"
                                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td16" runat="server" align="right">
                                                <asp:TextBox ID="TextBox16" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td10_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox17" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("YLBX")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td11_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox18" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("SYBX")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td12_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox19" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("YLIAOBX")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td13_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox20" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("YLIAODE")%>' onfocus="javascript:setToolTipGet(this);"
                                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td14_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox21" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("BJ")%>' onfocus="javascript:setToolTipGet(this);"
                                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td15_1" runat="server" align="right">
                                                <asp:TextBox ID="TextBox22" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("GJJ")%>'></asp:TextBox>
                                            </td>
                                            <td id="Td23" runat="server" align="right">
                                                <asp:TextBox ID="TB_FZ" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" Text='<%#Eval("GZ_FZ")%>' onfocus="javascript:setToolTipGet(this);"
                                                    onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td24" runat="server" align="right">
                                                <asp:TextBox ID="TextBox24" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td25" runat="server" align="right">
                                                <asp:TextBox ID="TextBox25" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td26" runat="server" align="right">
                                                <asp:TextBox ID="TextBox26" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td27" runat="server" align="right">
                                                <asp:TextBox ID="TextBox27" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                            <td id="Td28" runat="server" align="right">
                                                <asp:TextBox ID="TextBox28" runat="server" BorderStyle="None" BackColor="Transparent"
                                                    Width="50px" onfocus="javascript:setToolTipGet(this);" onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                没有记录!<br />
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
