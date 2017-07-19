<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_GSFPMANAGEMENT.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_GSFPMANAGEMENT" Title="工时管理页面" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
工时管理页面
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
     <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    <script type="text/javascript">
         
    </script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
        <div class="box-wrapper">
            <div class="box-outer">
                <table>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;
                            年月:<input type="text" style="width:80px" id="yearmonth" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;},editable:false" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                            机床编号:<input id="iptjcbh" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                            机床类型:<input id="iptjctype" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                            产品名称:<input id="iptcpname" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                         </td>
                        <td>
                            产品规格:<input id="iptcpguige" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                            总图号:<input id="ipttolmap" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                            部件图号:<input id="iptbjtuhao" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;
                            部件名称:<input id="iptbjname" class="easyui-textbox" runat="server" style="width:90px" />&nbsp;&nbsp;
                            <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">查询</a>
                            &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" />
                            <asp:RadioButton ID="radio_yijiesuan" runat="server" Text="未结算" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" Checked="true" />
                            <asp:RadioButton ID="radio_weijiesuan" runat="server" Text="已结算" GroupName="state" OnCheckedChanged="radiostate_CheckedChanged"
                                                AutoPostBack="True" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/TM_Data/TM_GSFPDETAIL.aspx?action=add" Target="_blank" runat="server">
                                <asp:Image ID="Image4" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                添加单据</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="box-wrapper">
                <div class="box-outer">
                    <div style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptgsmanagement" runat="server" OnItemDataBound="rptgsmanagement_itemdatabind">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            编号
                                        </td>
                                        <td align="center">
                                            机床编号
                                        </td>
                                        <td align="center">
                                            机床类型
                                        </td>
                                        <td align="center">
                                            年月
                                        </td>
                                        <td align="center">
                                            产品名称
                                        </td>
                                        <td align="center">
                                            产品规格
                                        </td>
                                        <td align="center">
                                            总图号
                                        </td>
                                        <td align="center">
                                            部件图号
                                        </td>
                                        <td align="center">
                                            部件名称
                                        </td>
                                        <td align="center">
                                            部件数量(定额)
                                        </td>
                                        <td>
                                            部件单件工时(定额)
                                        </td>
                                        <td>
                                            部件总工时(定额)
                                        </td>
                                        <td>
                                            部件数量(结算)
                                        </td>
                                        <td>
                                            部件单件工时(结算)
                                        </td>
                                        <td>
                                            部件总工时(结算)
                                        </td>
                                        <td>
                                            制单人
                                        </td>
                                        <td>
                                            制单时间
                                        </td>
                                        <td>
                                            结算状态
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                        <td align="center">
                                            查看
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td align="center">
                                            删除
                                        </td>
                                         <td align="center">
                                            结算
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                        <td align="center">
                                            <asp:CheckBox ID="cbxSelect" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_bh" runat="server" Text='<%#Eval("gs_bh")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_jcbh" runat="server" Text='<%#Eval("gs_jcbh")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_jctype" runat="server" Text='<%#Eval("gs_jctype")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_yearmonth" runat="server" Text='<%#Eval("gs_yearmonth")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_cpname" runat="server" Text='<%#Eval("gs_cpname")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_cpguige" runat="server" Text='<%#Eval("gs_cpguige")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_zongmap" runat="server" Text='<%#Eval("gs_zongmap")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_bjth" runat="server" Text='<%#Eval("gs_bjth")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_bjname" runat="server" Text='<%#Eval("gs_bjname")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_bjnum" runat="server" Text='<%#Eval("gs_bjnum")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_bjpergs" runat="server" Text='<%#Eval("gs_bjpergs")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_bjtotalgs" runat="server" Text='<%#Eval("gs_bjtotalgs")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_realbjnum" runat="server" Text='<%#Eval("gs_realbjnum")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_realbjpergs" runat="server" Text='<%#Eval("gs_realbjpergs")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_realbjtotalgs" runat="server" Text='<%#Eval("gs_realbjtotalgs")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_zdrname" runat="server" Text='<%#Eval("gs_zdrname")%>'></asp:Label>
                                            <asp:Label ID="gs_zdrid" runat="server" Visible="false" Text='<%#Eval("gs_zdrid")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_zdtime" runat="server" Text='<%#Eval("gs_zdtime")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="gs_state" runat="server" Text='<%#Eval("gs_state").ToString()=="0"?"已结算":"未结算"%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:TextBox ID="gs_note" TextMode="MultiLine" ToolTip='<%#Eval("gs_note")%>' Width="80px" Height="32px" runat="server" Text='<%#Eval("gs_note")%>'></asp:TextBox>
                                        </td>
                                        
                                        
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%#"~/TM_Data/TM_GSFPDETAIL.aspx?action=view&gs_bh="+Eval("gs_bh")%>' Target="_blank" runat="server">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />查看</asp:HyperLink>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%#"~/TM_Data/TM_GSFPDETAIL.aspx?action=edit&gs_bh="+Eval("gs_bh")%>' Target="_blank" runat="server" Visible= '<%#Eval("gs_state").ToString()=="0"?true:false%>'>
                                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:LinkButton ID="LinkButtonSC" OnClick="btndelete_OnClick" CommandArgument='<%# Eval("gs_bh")%>'  OnClientClick="return confirm('将删除该部件图号下的所有内容，确认删除吗?')" runat="server" Visible='<%#Eval("gs_state").ToString()=="0"?true:false%>'>
                                                <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />删除</asp:LinkButton>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkaccount" NavigateUrl='<%#"~/TM_Data/TM_GSFPDETAIL.aspx?action=account&gs_bh="+Eval("gs_bh")%>' Target="_blank" runat="server" Visible='<%#Eval("gs_state").ToString()=="0"?true:false%>'>
                                                <asp:Image ID="Image5" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />结算</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="10" align="right">
                                           合计：
                                        </td>
                                        <td>
                                           <asp:Label ID="lbdenumhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                           
                                        </td>
                                        <td>
                                           <asp:Label ID="lbdetolgshj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:Label ID="lbjsnumhj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lbjstolgshj" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </FooterTemplate>
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
</asp:Content>
