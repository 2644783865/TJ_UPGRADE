<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_FJCBIMPORT.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_FJCBIMPORT" Title="无标题页" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>分交成本信息</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#table1 input[type=checkbox]").attr("checked","true");
}
else{
 $("#table1 input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                全选/取消<input id="Checkbox2" type="checkbox" />
                            </td>
                            <td style="width: 23%;">
                                <strong>时间：</strong>
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td align="center">
                                <strong>油漆种类：</strong><asp:TextBox ID="tbzl" runat="server"></asp:TextBox>
                            </td>
                            <td align="center" style="width:26%">
                                <strong>任务号:</strong><asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td style="width:20%" align="left">
                                <asp:Button ID="Button1" runat="server" Text="导出数据" OnClick="btn_export_Click" />
                            </td>
                          </tr>  
                    </table>
                </div>
            </div>
        </div>
                <div class="box-outer">
                    <table width="98%">
                       <tr>
                            <td style="width:20%">
                                <asp:FileUpload runat="server" ID="FileUpload1" Width="200px" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btn" runat="server" Text="导入数据" OnClick="btn_import_Click" />
                            </td>
                            <td>
                              <strong>年份:</strong>
                                <asp:DropDownList ID="Dpyear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                <strong>月份:</strong>
                                <asp:DropDownList ID="Dpmonth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyLHZ" runat="server" NavigateUrl="FM_FJCBHZ.aspx"><strong>按任务号汇总</strong></asp:HyperLink>
                            </td>
                            <td align="right" style="width: 200px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('FM_FJCB_ADD.aspx?','','dialogWidth=650px;dialogHeight=400px');" runat="server">
                          <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             添加</asp:HyperLink>&nbsp;&nbsp;
                            </td>
                            <td align="right">
                             <asp:Button ID="btnSC" runat="server" Text="删除" OnClientClick="javascript:return confirm('确定要删除吗？');" onclick="btnSC_Click"  />
                            </td>
                          </tr>
                    </table>
                  </div>
                <div  style="overflow: scroll;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%" >
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor">
                                <th>
                                    序号
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    油漆种类
                                </th>
                                <th>
                                    用量（L）
                                </th>
                                <th>
                                    单价（含税）
                                </th>
                                <th>
                                    金额（含税）
                                </th>
                                <th>
                                    稀释剂用量（L）
                                </th>
                                <th>
                                    稀释剂单价（含税）
                                </th>
                                <th>
                                    稀释剂金额（含税）
                                </th>
                                <th>
                                    合计金额（含税）
                                </th>
                                <th>
                                    合计不含税金额
                                </th>
                                <th>
                                    年月份
                                </th>
                                <th>修改信息</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <asp:Label ID="lblID" runat="server" visible="false" Text='<%#Eval("ID")%>'></asp:Label>
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="chkDel"
                                        runat="server" />    
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("FJCB_TSAID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyqzl" runat="server" Text='<%#Eval("FJCB_YQZL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyqyl" runat="server" Text='<%#Eval("FJCB_YQYL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyqhsdj" runat="server" Text='<%#Eval("FJCB_YQHSDJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyqhsje" runat="server" Text='<%#Eval("FJCB_YQHSJE")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbxsjyl" runat="server" Text='<%#Eval("FJCB_XSJYL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbxsjhsdj" runat="server" align="center" Text='<%#Eval("FJCB_XSJHSDJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbxsjhsje" runat="server" align="center" Text='<%#Eval("FJCB_XSJHSJE")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhjhsje" runat="server" align="center" Text='<%#Eval("FJCB_HJHSJE")%>'></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbhjbhsje" runat="server" align="center" Text='<%#Eval("FJCB_HJBHSJE")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbny" runat="server" align="center" Text='<%#Eval("FJCB_YEARMONTH")%>'></asp:Label>
                                </td>
                                <td><asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%# editDq(Eval("ID").ToString()) %>'  runat="server" >
                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                修改</asp:HyperLink>
                            </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="2" align="right">
                                    合计：
                                </td>
                                <td align="center">

                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyqylzj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyqhsjezj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbxsjylzj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbxsjhsjezj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhsjezj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbbhsjezj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
