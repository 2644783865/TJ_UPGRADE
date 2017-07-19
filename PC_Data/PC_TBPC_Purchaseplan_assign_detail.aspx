<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_assign_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_assign_detail"
    Title="任务分工" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购计划单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">
    function document.onreadystatechange()
    {
      if (document.readyState=="complete") 
      {
        var ptc="<%=gloabptc%>";
        var col=2;
        othptcfind(ptc,col)
      }
    }
    function redirectw(obj)
    {
        var sheetno;
        sheetno=obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
        //window.location.href="PC_TBPC_Purchaseplan_assign_detail.aspx?sheetno="+sheetno;
        //window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+sheetno;
    }
    function confirmwinfr()
    {
        var a;
        a=confirm("确定取消?");
        return a;
    }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        采购任务分工:
                                        <asp:RadioButton ID="rad_all" runat="server" Text="全部" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="rad_wfg" runat="server" Text="未分工" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_wfg_CheckedChanged" AutoPostBack="true" Checked="true" />
                                        <asp:RadioButton ID="rad_yfg" runat="server" Text="已分工" TextAlign="Right" GroupName="select11"
                                            OnCheckedChanged="rad_yfg_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="finish" runat="server" Text="完成" OnClick="finish_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_fanshen" runat="server" Text="取消分工" />
                                        <%-- OnClick="btn_fanshen_Click"--%>
                                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    编&nbsp;&nbsp;&nbsp;号：<asp:TextBox ID="TextBox_pid" runat="server" Text="" Enabled="false"
                                        Width="150px"></asp:TextBox>
                                    <asp:TextBox ID="Label_view" runat="server" Visible="false" Text=""></asp:TextBox>
                                </td>
                                <td align="left">
                                    日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="Tb_shijian" runat="server" Text="" Enabled="false"
                                        Width="150px"></asp:TextBox>
                                </td>
                                <td align="left">
                                    材 &nbsp;料 &nbsp;类 &nbsp;别：<asp:DropDownList ID="DropDownList_TY" runat="server" DataTextField="PUR_TY_NAME"
                                        DataValueField="PUR_TY_ID" OnSelectedIndexChanged="DropDownList_TY_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div style="height: 450px; overflow: auto; width: 100%">
                            <div class="cpbox1 xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="tbpc_purshaseplanrealityRepeater" runat="server" OnItemDataBound="tbpc_purshaseplanrealityRepeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>物料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>名称</strong>
                                                </td>
                                                <td>
                                                    <strong>材料类型</strong>
                                                </td>
                                                <td>
                                                    <strong>材质</strong>
                                                </td>
                                                <td>
                                                    <strong>规格</strong>
                                                </td>
                                                <td>
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>图号</strong>
                                                </td>
                                                <td>
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td runat="server" id="fznum1">
                                                    <strong>辅助数量</strong>
                                                </td>
                                                <td runat="server" id="fzunit1">
                                                    <strong>辅助单位</strong>
                                                </td>
                                                <td>
                                                    <strong>申请人</strong>
                                                </td>
                                                <td>
                                                    <strong>采购员</strong>
                                                </td>
                                                <td id="td1" runat="server" style="display: none">
                                                    <strong>分工人</strong>
                                                </td>
                                                <td>
                                                    <strong>分工标志</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <td>
                                                    <strong>长度</strong>
                                                </td>
                                                <td>
                                                    <strong>宽度</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" ondblclick="redirectw(this)">
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        onclick="checkme(this)"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PUR_PTCODE" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;
                                                        text-align: center" Width="150px" Text='<%#Eval("PUR_PTCODE")%>' ToolTip='<%#Eval("PUR_PTCODE")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="fznum2">
                                                    <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("PUR_RPFZNUM")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td runat="server" id="fzunit2">
                                                    <asp:Label ID="PUR_FZUNIT" runat="server" Text='<%#Eval("PUR_FZUNIT")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <%#Eval("sqrnm")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_CGMANNAME" runat="server" Text='<%#Eval("PUR_CGMANNAME")%>'></asp:Label>
                                                    <asp:Label ID="PUR_CGMANCODE" runat="server" Text='<%#Eval("PUR_CGMANCODE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td id="td1" runat="server" style="display: none">
                                                    <asp:Label ID="PUR_PTASMAN" runat="server" Text='<%#Eval("PUR_PTASMAN")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_fg_state(Eval("PUR_STATE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                </td>
                                                
                                                
                                                
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="19" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" align="center">
                                    <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                        Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                                    <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                                    <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="Labelddl_caigouyuan" runat="server" Text="为选定的材料选择采购员："></asp:Label>
                                    <asp:DropDownList ID="ddl_caigouyuan" AutoPostBack="true" runat="server" DataTextField="PUR_ST_NAME"
                                        DataValueField="PUR_ST_CODE" OnSelectedIndexChanged="ddl_caigouyuan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Button ID="Button_save" runat="server" Text="保存" OnClick="Button_save_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
