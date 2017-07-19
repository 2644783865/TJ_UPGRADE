<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_EATNEWLIST.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_EATNEWLIST" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
用餐申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function(){
            $("#Checkbox2").click(function(){
            if($("#Checkbox2").attr("checked")){
             $("#tab input[type=checkbox]").attr("checked","true");
            }
            else{
             $("#tab input[type=checkbox]").removeAttr("checked");
            }
            });})
    </script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radio_all_CheckedChanged"
                                            AutoPostBack="True" />
                            <asp:RadioButton ID="radio_mytask" runat="server" Text="我的任务" GroupName="shenhe" OnCheckedChanged="radio_mytask_CheckedChanged"
                                            AutoPostBack="True"  Checked="true"/>
                        
                        &nbsp;&nbsp;&nbsp;用餐类型：
                        <asp:RadioButton ID="radiowplb_all" runat="server" Text="全部" GroupName="leibie" OnCheckedChanged="radiowplb_CheckedChanged"
                                            AutoPostBack="True"  Checked="true" />
                        <asp:RadioButton ID="radiowplb_yc" runat="server" Text="用餐" GroupName="leibie" OnCheckedChanged="radiowplb_CheckedChanged"
                                        AutoPostBack="True"/>
                        <asp:RadioButton ID="radiowplb_yp" runat="server" Text="饮品" GroupName="leibie" OnCheckedChanged="radiowplb_CheckedChanged"
                                            AutoPostBack="True"/>
                        
                        &nbsp;&nbsp;&nbsp;报销状态：
                        <asp:RadioButton ID="radio_alldata" runat="server" Text="全部" GroupName="baoxiao" OnCheckedChanged="radiobaoxiaostate_CheckedChanged"
                                            AutoPostBack="True" />
                        <asp:RadioButton ID="radio_weibaoxiao" runat="server" Text="未报销" GroupName="baoxiao" OnCheckedChanged="radiobaoxiaostate_CheckedChanged"
                                        AutoPostBack="True"  Checked="true"/>
                        <asp:RadioButton ID="radio_yibaoxiao" runat="server" Text="已报销" GroupName="baoxiao" OnCheckedChanged="radiobaoxiaostate_CheckedChanged"
                                            AutoPostBack="True"/>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        审批状态：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="0">初始化</asp:ListItem>
                                            <asp:ListItem Value="1">审核中</asp:ListItem>
                                            <asp:ListItem Value="2">已通过</asp:ListItem>
                                            <asp:ListItem Value="3">已驳回</asp:ListItem>
                                  </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        日期从：<input type="text" id="startdate" style="width:100px" runat="server" class="easyui-datebox" />&nbsp;&nbsp;&nbsp;
                        到：<input type="text" id="enddate" runat="server"  style="width:100px" class="easyui-datebox" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                        &nbsp;&nbsp;&nbsp;
                   </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnbaoxiao" runat="server" Text="报销确认" OnClick="btnbaoxiao_Click"></asp:Button>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnqxbaoxiao" runat="server" Text="取消报销状态" OnClick="btnqxbaoxiao_Click"></asp:Button>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnexport" runat="server" Text="导出" OnClick="btnexport_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/OM_Data/OM_EATNEW.aspx?" Target="_blank" Font-Underline="false"><asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />新增用餐申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptycsq" runat="server">
                                <HeaderTemplate>
                                    <tr style="background-color: #B9D3EE;" height="30px">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            编号
                                        </td>
                                        <td align="center">
                                            用餐部门
                                        </td>
                                        <td align="center">
                                            用餐时间
                                        </td>
                                        <td align="center">
                                            用餐人数
                                        </td>
                                        <td align="center">
                                            用餐规格
                                        </td>
                                        <td align="center">
                                            用餐金额
                                        </td>
                                        <td align="center">
                                            用餐类型
                                        </td>
                                        <td align="center">
                                            类别(用餐/饮品)
                                        </td>
                                        <td align="center">
                                            申请人
                                        </td>
                                        <td align="center">
                                            申请时间
                                        </td>
                                        <td align="center">
                                            申请人电话
                                        </td>
                                        <td align="center">
                                            审批状态
                                        </td>
                                        <td align="center">
                                            查看/审核
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            报销状态
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" height="30px">
                                        <td>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                            <asp:Label ID="IDdetail" runat="server" Text='<%#Eval("IDdetail")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="eatycbh" runat="server" Text='<%#Eval("eatbh")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td id="tdeatbh" runat="server" align="center">
                                            <%#Eval("eatbh")%>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="DEP_NAME" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="eatyctime" runat="server" Text='<%#Eval("eatyctime")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="detailnum" runat="server" Text='<%#Eval("detailnum")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="detailprice" runat="server" Text='<%#Eval("detailprice")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="detailmoney" runat="server" Text='<%#Eval("detailmoney")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="eattype" runat="server" Text='<%#Eval("eattype").ToString()=="1"?"加班餐":"客饭"%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="detailifYP" runat="server" Text='<%#Eval("detailifYP").ToString()=="2"?"用餐":"饮品"%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="eatsqrname" runat="server" Text='<%#Eval("eatsqrname")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="eatsqtime" runat="server" Text='<%#Eval("eatsqtime")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="eatsqrphone" runat="server" Text='<%#Eval("eatsqrphone")%>'></asp:Label>
                                        </td>
                                        
                                        <td runat="server" align="center">
                                            <asp:Label ID="eatstate" runat="server" Text='<%#Eval("eatstate").ToString()=="0"?"初始化":Eval("eatstate").ToString()=="1"?"审核中":Eval("eatstate").ToString()=="2"?"通过":Eval("eatstate").ToString()=="3"?"驳回":"出错"%>'></asp:Label>
                                        </td>
                                        <td  id="tdcksh" runat="server" align="center">
                                        <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看/审核" NavigateUrl='<%#"~/OM_Data/OM_EATSPdetailnew.aspx?spid="+Eval("eatbh") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    查看/审核
                                </asp:HyperLink>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="eatsqrnote" runat="server" Text='<%#Eval("eatsqrnote")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbbaoxiaostate" runat="server" Text='<%#Eval("baoxiaostate").ToString().Trim()==""?"否":"是"%>'></asp:Label>
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
            
            <table width="100%" id="table1">
                        <tr>
                            <td align="left">
                                <input type="checkbox" id="Checkbox2" />全选&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                合计金额：<asp:Label ID="lb_totalmoney" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>元&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
             </table>
</asp:Content>
