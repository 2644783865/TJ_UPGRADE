<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchange_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchange_detail"
    Title="变更管理" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    变更执行
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

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
                                    <td>
                                        <asp:TextBox ID="pt_code" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <%--<asp:Button ID="Button1" runat="server" Text="关闭" />&nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="备库" />&nbsp;
                                        <asp:Button ID="Button3" runat="server" Text="确定" />&nbsp;--%>
                                     <%--   <asp:Button ID="Button4" runat="server" Text="返回" />&nbsp;--%>
                                        <a href="javascript:history.go(-1);">向上一页</a> &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <%-- <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;目：
                                        <asp:TextBox ID="Tb_pcode" runat="server" Text="" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="DropDownList_PJ" runat="server" DataTextField="PUR_PJNAME"
                                            DataValueField="PUR_PJID" OnSelectedIndexChanged="DropDownList_PJ_SelectedIndexChanged"
                                            AutoPostBack="True" Width="150px">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="Tb_PJID" runat="server" Text="" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="Tb_PJNAME" runat="server" Text="" Visible="false"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;程：<asp:DropDownList ID="downlist_eng" runat="server"
                                            AutoPostBack="true" DataTextField="PUR_ENGNAME" DataValueField="PUR_ENGID" OnSelectedIndexChanged="downlist_eng_SelectedIndexChanged"
                                            Width="150px">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="Tb_ENGID" runat="server" Text="" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="Tb_ENGNAME" runat="server" Text="" Visible="false"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 34%;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 33%;">
                                        物料类别：
                                        <asp:DropDownList ID="DropDownList_TY" runat="server" DataTextField="PUR_TY_NAME"
                                            DataValueField="PUR_TY_ID" OnSelectedIndexChanged="DropDownList_TY_SelectedIndexChanged"
                                            AutoPostBack="True" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width: 33%;">
                                        &nbsp;&nbsp;&nbsp;采购员：<asp:TextBox ID="PUR_CGMANNAME" runat="server" Enabled="false"
                                            Width="150px"></asp:TextBox><asp:TextBox ID="PUR_CGMAN" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="Tb_shijian" runat="server"
                                            Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>--%>
                        <div style="overflow: scroll; width: 100%; height: 100%">
                            <div class="cpbox xscroll">
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td style="font-size: small; text-align: center;" colspan="4">
                                                采购计划
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="tbpc_puryclRepeater" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color:#5CACEE"> 
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>批号</strong>
                                                </td>
                                                <td>
                                                    <strong>项目</strong>
                                                </td>
                                                <td>
                                                    <strong>工程</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>材料ID</strong>
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
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>计划数量</strong>
                                                </td>
                                              
                                                <td>
                                                    <strong>采购数量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>长度</strong>
                                                </td>
                                                <td>
                                                    <strong>宽度</strong>
                                                </td>
                                                
                                                <td>
                                                    <strong>采购人</strong>
                                                </td>
                                                <td>
                                                    <strong>状态</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <%--<asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false"></asp:CheckBox>&nbsp;--%>
                                                </td>
                                                <td id="bpihao" visible="false">
                                                    <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                  
                                                    <asp:Label ID="PUR_PJID" runat="server" Text='<%#Eval("PUR_PJID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_PJNAME" runat="server" Text='<%#Eval("PUR_PJNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   
                                                    <asp:Label ID="PUR_ENGID" runat="server" Text='<%#Eval("PUR_ENGID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_ENGNAME" runat="server" Text='<%#Eval("PUR_ENGNAME")%>'></asp:Label>
                                                </td>
                                                <td> 
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   
                                                    <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("PUR_NUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                  
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("PUR_LENGTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("PUR_WIDTH")%>'></asp:Label>
                                                </td>
                                                
                                                <td>
                                                    
                                                    <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="PUR_STATE" runat="server" Text=""></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        <tr align="center">
                                            <td colspan="18">
                                                没有记录！
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: small; text-align: center;" colspan="4">
                                                变更信息
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="tbpc_purbgclRepeater" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>变更批号</strong>
                                                </td>
                                                <td>
                                                    <strong>项目</strong>
                                                </td>
                                                <td>
                                                    <strong>工程</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>材料ID</strong>
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
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>状态</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <%--<asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false"></asp:CheckBox>&nbsp;--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_PCODE" runat="server" Text='<%#Eval("BG_PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                 
                                                    <asp:Label ID="MP_PJID" runat="server" Text='<%#Eval("MP_PJID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="MP_PJNAME" runat="server" Text='<%#Eval("MP_PJNAME")%>'></asp:Label>
                                                </td>
                                                <td> 
                                                    <asp:Label ID="MP_ENGID" runat="server" Text='<%#Eval("MP_ENGID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="MP_ENGNAME" runat="server" Text='<%#Eval("MP_ENGNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_PTCODE" runat="server" Text='<%#Eval("BG_PTCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="BG_MARID" runat="server" Text='<%#Eval("BG_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                   
                                                    <asp:Label ID="BG_MARNAME" runat="server" Text='<%#Eval("BG_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                  
                                                    <asp:Label ID="BG_MARNORM" runat="server" Text='<%#Eval("BG_MARNORM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                 
                                                    <asp:Label ID="BG_MARTERIAL" runat="server" Text='<%#Eval("BG_MARTERIAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                 
                                                    <asp:Label ID="BG_GUOBIAO" runat="server" Text='<%#Eval("BG_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                
                                                    <asp:Label ID="BG_NUNIT" runat="server" Text='<%#Eval("BG_NUNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                              
                                                    <asp:Label ID="BG_NUM" runat="server" Text='<%#Eval("BG_NUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                             
                                                    <asp:Label ID="BG_FZNUM" runat="server" Text='<%#Eval("BG_FZNUM")%>'></asp:Label>
                                                </td>
                                                 <td>
                                             
                                                    <asp:Label ID="MP_STATE" runat="server" Text='<%#Eval("MP_STATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:Label ID="BG_NOTE" runat="server" Text='<%#Eval("BG_NOTE")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     <asp:Panel ID="NoDataPane2" runat="server" Visible="false">
                                        <tr align="center">
                                            <td colspan="13">
                                                没有记录！
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
