<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="YS_Order_Detail.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Order_Detail" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    订单明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .NoNewline
        {
            word-break: keep-all; /*必须*/
        }
    </style>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="YS_NAME" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div>
            <div>
                <div style="background-color: #FAFAFA">
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <asp:RadioButtonList ID="rab_daohuo" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rab_daohuo_selectchanged" AutoPostBack="true">
                                    <asp:ListItem Selected="True">全部</asp:ListItem>
                                    <asp:ListItem>未提交</asp:ListItem>
                                    <asp:ListItem>未到货</asp:ListItem>
                                    <asp:ListItem>逾期未到货</asp:ListItem>
                                    <asp:ListItem>部分到货</asp:ListItem>
                                    <asp:ListItem>已到货</asp:ListItem>
                                    <asp:ListItem>逾期到货</asp:ListItem>
                                    <asp:ListItem>已关闭</asp:ListItem>
                                    <asp:ListItem>已删除</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="left">
                                订单编号：
                                <asp:TextBox ID="tb_orderno" ToolTip="多个订单请用减号“-”分隔" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td colspan="2" align="left">
                                计划跟踪号：
                                <asp:TextBox ID="tb_ptc" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td colspan="1" align="right">
                                物料类型：
                            </td>
                            <td colspan="1">
                                <asp:DropDownList ID="DropDownList_mar_type" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td colspan="1" align="right">
                                质检结果：
                            </td>
                            <td colspan="1">
                                <asp:DropDownList ID="DropDownList_check_result" runat="server">
                                    <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                    <asp:ListItem Text="未报检" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="待检" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="报废" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="整改" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="待定" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="让步接收" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="部分合格" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="合格" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="未审核" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="已审核" Value="9"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="QueryButton1" runat="server" OnClick="QueryButton_Click" Text="查询" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="height: 430px; overflow: auto; width: 100%">
                    <div class="cpbox xscroll">
                        <table id="tab" align="center" border="1" cellspacing="1" frame="border" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="Purordertotal_list_Repeater" OnItemDataBound="Purordertotal_list_Repeater_ItemDataBound"
                                runat="server">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                        <td class="NoNewline">
                                            <strong>行号</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>订单编号</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>销售合同号</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>项目/工程名称</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>项目</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>供应商</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>材料编码</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>名称</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>规格</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>材质</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>国标</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>数量</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>单位</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>含税单价</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>含税金额</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>类型</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>长度</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>宽度</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>图号/标识号</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>总金额</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>制单人</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>制单日期</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>交货日期</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>质量报检</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>入库情况</strong>
                                        </td>
                                        <%--<td class="NoNewline">
                                            <strong>订单请款单号</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>订单发票</strong>
                                        </td>--%>
                                        <td class="NoNewline">
                                            <strong>实际到货日期</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>审核标志</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>备注</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>计划跟踪号</strong>
                                        </td>
                                        <td class="NoNewline">
                                            <strong>到货数量</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                        style="background-color: #EAEAEA">
                                        <td class="NoNewline" height="20">
                                            <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_CODE" runat="server" Text='<%#Eval("orderno")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="salescontract" runat="server" Text='<%#Eval("salescontract").ToString()==""?"未添加":Eval("salescontract").ToString()%>'
                                                ForeColor='<%#Eval("salescontract").ToString()==""?System.Drawing.Color.Red:System.Drawing.Color.Blue%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="engnm" runat="server" Text='<%#Eval("engnm")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="pjnm" runat="server" Text='<%#Eval("pjnm")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_SUPPLIERID" runat="server" Text='<%#Eval("supplierid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PO_SUPPLIERNM" runat="server" Text='<%#Eval("suppliernm")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="marid" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="margg" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="marcz" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="margb" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="zxnum" runat="server" Text='<%#Eval("zxnum")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="marunit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="ctprice" runat="server" Text='<%#Eval("ctprice")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="ctamount" runat="server" Text='<%#Eval("ctamount")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_MASHAPE" runat="server" Text='<%#Eval("PO_MASHAPE")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="length" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="width" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_TUHAO" runat="server" Text='<%#Eval("PO_TUHAO")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="zje" runat="server" Text='<%#Eval("PO_ZJE")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_ZDID" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PO_ZDNM" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_SHTIME" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"zdtime")).ToShortDateString()%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="cgtimerq" runat="server" Text='<%#Eval("cgtimerq")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="zlbj" runat="server" Text='<%#Eval("PO_CGFS").ToString()%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="rukuF" runat="server"></asp:Label>
                                        </td>
                                       <%-- <td class="NoNewline">
                                            <asp:Label ID="ddqkcode" runat="server" Text='<%#Eval("PO_CRCODE").ToString()==""?"未添加":Eval("PO_CRCODE").ToString()%>'
                                                ForeColor='<%#Eval("PO_CRCODE").ToString()==""?System.Drawing.Color.Red:System.Drawing.Color.Blue%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="ddbillcode" runat="server" Text='<%#Eval("PO_BILL").ToString()==""?"未添加":Eval("PO_BILL").ToString()%>'
                                                ForeColor='<%#Eval("PO_BILL").ToString()==""?System.Drawing.Color.Red:System.Drawing.Color.Blue%>'></asp:Label>
                                        </td>--%>
                                        <td class="NoNewline">
                                            <asp:Label ID="recdate" runat="server" Text='<%#Eval("recdate")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_shbz" runat="server" Text='<%#Eval("totalstate").ToString()%>'></asp:Label>
                                            <asp:Label ID="PO_STATE" runat="server" Text='<%#Eval("totalstate")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PO_cstate" runat="server" Text='<%#Eval("detailcstate")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="detailstate" runat="server" Text='<%#Eval("detailstate")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="PO_NOTE" runat="server" Text='<%#Eval("totalnote")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                        </td>
                                        <td class="NoNewline">
                                            <asp:Label ID="recgdnum" runat="server" Text='<%#Eval("recgdnum")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <td align="center" colspan="11" class="NoNewline" bgcolor="#F5FFFA">
                                        <strong>合计</strong>
                                    </td>
                                    <td>
                                    <asp:Label ID="YS_mar_num" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                    <asp:Label ID="YS_average_price" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="1" class="NoNewline" bgcolor="#F5FFFA">
                                        <asp:Label ID="YS_mar_amount" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="NoNewline" colspan="19" bgcolor="#F5FFFA">
                                    </td>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="NoDataPanel" runat="server">
                                没有记录!</asp:Panel>
                        </table>
                    </div>
                </div>
                <div>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
