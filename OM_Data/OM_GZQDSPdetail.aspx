<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_GZQDSPdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GZQDSPdetail" Title="工资清单审批明细" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
工资清单审批明细
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
    <script language="javascript" type="text/javascript">
        window.onload=function(){
           var tab = document.getElementById("tab");
           for (i = 3; i < (tab.rows.length-1); i++)
            {
                var cols = tab.rows[i].cells.length;
                for(var m = 10; m < cols; m++)
                {
                   if(tab.rows[i].getElementsByTagName("td")[m].getElementsByTagName("span")[0].innerHTML=="0")
                   {
                       tab.rows[i].getElementsByTagName("td")[m].getElementsByTagName("span")[0].innerHTML="";
                   }
                }
            }
            var footnum=tab.rows.length-1;
            for(var n = 4; n < cols; n++)
            {
                   if(tab.rows[footnum].getElementsByTagName("td")[n].getElementsByTagName("span")[0].innerHTML=="0")
                   {
                       tab.rows[footnum].getElementsByTagName("td")[n].getElementsByTagName("span")[0].innerHTML="";
                   }
           }
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
                        <asp:Button runat="server" ID="btnfanshen" Text="反审" OnClick="btnfanshen_OnClick" Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
               </tr>
         </table>
    </div>
    <div class="box_right">
        <div class="box-inner">
            <table style="width: 100%">
                <tr>
                    <td width="15%">
                        <strong>部门：</strong>&nbsp;
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td width="20%">
                        每页显示记录条数:
                        <asp:DropDownList ID="DropDownListCount" runat="server" Width="60px" OnSelectedIndexChanged="Count_Change" AutoPostBack="true">
                                                <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td width="15%">
                         <asp:HyperLink ID="HyperLinksx" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2" 
                                            align="absmiddle" runat="server" />筛选</asp:HyperLink>
                         <asp:PopupControlExtender ID="PopupControlExtendersx" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-300"  OffsetY="8"  TargetControlID="HyperLinksx" PopupControlID="palORG">
                         </asp:PopupControlExtender>
                          <asp:Panel ID="palORG" Width="500px" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                         <table width="100%" >
                                 <tr>       
                                     <td>
                                          <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                                              <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                          </div>
                                          <br /><br />
                                     </td>
                                 </tr>
                         
                                 <tr>
                                     <td>
                                         姓名：
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                     </td>
                                     <td>
                                         工号：
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtworkno" runat="server"></asp:TextBox>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         班组：
                                     </td>
                                     <td>
                                         <asp:DropDownList ID="DropDownListbanzu" runat="server" Width="100px" AutoPostBack="false">
                                         </asp:DropDownList>
                                     </td>
                                     <td>
                                         岗位：
                                     </td>
                                     <td>
                                         <asp:DropDownList ID="DropDownListgw" runat="server" Width="100px" AutoPostBack="false">
                                         </asp:DropDownList>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         固定工资从：
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtgdgzmin" runat="server"></asp:TextBox>
                                     </td>
                                     <td>
                                         到：
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtgdgzmax" runat="server"></asp:TextBox>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td colspan="4" align="center">
                                         <asp:Button ID="btnQuery" runat="server" UseSubmitBehavior="false" Text="查询" OnClick="btnQuery_OnClick"></asp:Button>
                                     </td>
                                     
                                 </tr>
                             </table>
                        </asp:Panel>  
                    </td>
                    <td colspan="2" align="right">
                        勾选隐藏列：
                        <asp:CheckBox ID="cbxBumen" runat="server" AutoPostBack="true" OnCheckedChanged="cbxBumen_CheckedChanged" />
                        部门 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="cbxKaoqin" runat="server" AutoPostBack="true" OnCheckedChanged="cbxKaoqin_CheckedChanged" />
                        考勤 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="cbxWXYJ" runat="server" AutoPostBack="true" OnCheckedChanged="cbxWXYJ_CheckedChanged" />
                        五险一金&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" ActiveTabIndex="0" >
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="工资清单明细" TabIndex="0">
            <ContentTemplate>
            <div style="overflow: scroll;height: 500px;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1"
                    border="1">
                               <tr align="center">
                                  <td align="center" colspan="48" style="border:none">
                                      工资清单<asp:Label ID="lb_title" runat="server"></asp:Label>
                                  </td>
                              </tr>
                              <tr align="center">
                                  <td align="left" colspan="48" style="border:none">
                                      制单人：<asp:Label ID="lbtitle_zdr" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单时间：<asp:Label ID="lbtitle_zdsj" runat="server"></asp:Label>
                                  </td>
                              </tr>
                               <asp:Repeater ID="rptGZQD" runat="server" OnItemDataBound="rptGZQD_ItemDataBound">
                                     <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;text-overflow:ellipsis;white-space:nowrap;">
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
                                            工号
                                        </td>
                                        <td>
                                            姓名
                                        </td>
                                        <td>
                                            合同主体
                                        </td>
                                        <td>
                                            区分标识
                                        </td>
                                        <td runat="server" id="tdBumen">
                                            部门
                                        </td>
                                        <td runat="server" id="tdBanzu">
                                            班组
                                        </td>
                                        <td>
                                            岗位
                                        </td>
                                        <td runat="server" id="tdYCQHJ">
                                            出勤
                                        </td>
                                        <td runat="server" id="tdJRwork">
                                            节加
                                        </td>
                                        <td runat="server" id="tdZhouwork">
                                            周加
                                        </td>
                                        <td runat="server" id="tdRiwork">
                                            延时
                                        </td>
                                        <td runat="server" id="tdBingjia">
                                            病假
                                        </td>
                                        <td runat="server" id="tdShijia">
                                            事假
                                        </td>
                                        <td runat="server" id="tdNianjia">
                                            年假
                                        </td>
                                        <td>
                                            基础工资
                                        </td>
                                        <td>
                                            工龄工资
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
                                            加班补发
                                        </td>
                                        <td>
                                            中夜班费
                                        </td>
                                        <td>
                                            中夜班补发
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
                                            交通补贴
                                        </td>
                                        <td>
                                            防暑降温费
                                        </td>
                                        <td>
                                            采暖补贴
                                        </td>
                                        <td>
                                            其他
                                        </td>
                                        <td>
                                            应付合计
                                        </td>
                                        <td runat="server" id="tdYangLBX">
                                            养老保险
                                        </td>
                                        <td runat="server" id="tdSYBX">
                                            失业保险
                                        </td>
                                        <td runat="server" id="tdYiLBX">
                                            医疗保险
                                        </td>
                                        <td runat="server" id="tdDEJiuZhu">
                                            大额救助
                                        </td>
                                        <td runat="server" id="tdBuBX">
                                            补保险
                                        </td>
                                        <td runat="server" id="tdGJJ">
                                            公积金
                                        </td>
                                        <td runat="server" id="tdBGJJ">
                                            补公积金
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
                                            扣税基数
                                        </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" style="text-overflow:ellipsis;white-space:nowrap;">
                                        <td>
                                            <asp:Label runat="server" ID="lbQD_ID" Visible="false" Text='<%#Eval("QD_ID")%>'></asp:Label>
                                            <asp:CheckBox ID="cbxNumber" CssClass="checkBoxCss" runat="server" />
                                            <%# Container.ItemIndex + 1 + (Convert.ToDouble(UCPaging1.CurrentPage) - 1) * (Convert.ToDouble(UCPaging1.PageSize))%>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lbQD_SHBH" runat="server" Text='<%#Eval("QD_SHBH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_YEARMONTH" runat="server" Text='<%#Eval("QD_YEARMONTH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_Worknumber" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_Name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_HTZT" runat="server" Text='<%#Eval("QD_HTZT")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_QuFen" runat="server" Text='<%#Eval("QD_QFBS")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BuMen" runat="server" align="center">
                                            <asp:Label ID="lbQD_BuMen" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BanZu" runat="server" align="center">
                                            <asp:Label ID="lbQD_BanZu" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GangWei" runat="server" Text='<%#Eval("DEP_NAME_POSITION")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td id="tdKQ_CHUQIN" runat="server" align="center">
                                            <asp:Label ID="lbKQ_CHUQIN" runat="server" Text='<%#Eval("KQ_CHUQIN")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_JRJIAB" runat="server" align="center">
                                            <asp:Label ID="lbKQ_JRJIAB" runat="server" Text='<%#Eval("KQ_JRJIAB")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_ZMJBAN" runat="server" align="center">
                                            <asp:Label ID="lbKQ_ZMJBAN" runat="server" Text='<%#Eval("KQ_ZMJBAN")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_YSGZ" runat="server" align="center">
                                            <asp:Label ID="lbKQ_YSGZ" runat="server" Text='<%#Eval("KQ_YSGZ")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_BINGJ" runat="server" align="center">
                                            <asp:Label ID="lbKQ_BINGJ" runat="server" Text='<%#Eval("KQ_BINGJ")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_SHIJ" runat="server" align="center">
                                            <asp:Label ID="lbKQ_SHIJ" runat="server" Text='<%#Eval("KQ_SHIJ")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_NIANX" runat="server" align="center">
                                            <asp:Label ID="lbKQ_NIANX" runat="server" Text='<%#Eval("KQ_NIANX")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td align="center">
                                            <asp:Label ID="lbQD_JCGZ" runat="server" Text='<%#Eval("QD_JCGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GZGL" runat="server" Text='<%#Eval("QD_GZGL")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GDGZ" runat="server" Text='<%#Eval("QD_GDGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JXGZ" runat="server" Text='<%#Eval("QD_JXGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JiangLi" runat="server" Text='<%#Eval("QD_JiangLi")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_BingJiaGZ" runat="server" Text='<%#Eval("QD_BingJiaGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JiaBanGZ" runat="server" Text='<%#Eval("QD_JiaBanGZ")%>'></asp:Label>
                                        </td>
                                         <td align="center">
                                            <asp:Label ID="lbQD_BFJB" runat="server" Text='<%#Eval("QD_BFJB")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_ZYBF" runat="server" Text='<%#Eval("QD_ZYBF")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_BFZYB" runat="server" Text='<%#Eval("QD_BFZYB")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_NianJiaGZ" runat="server" Text='<%#Eval("QD_NianJiaGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_YKGW" runat="server" Text='<%#Eval("QD_YKGW")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_TZBF" runat="server" Text='<%#Eval("QD_TZBF")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_TZBK" runat="server" Text='<%#Eval("QD_TZBK")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JTBT" runat="server" Text='<%#Eval("QD_JTBT")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_FSJW" runat="server" Text='<%#Eval("QD_FSJW")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_CLBT" runat="server" Text='<%#Eval("QD_CLBT")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_QTFY" runat="server" Text='<%#Eval("QD_QTFY")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_YFHJ" runat="server" Text='<%#Eval("QD_YFHJ")%>'></asp:Label>
                                        </td>
                                        
                                        

                                        <td id="tdQD_YLBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_YLBX" runat="server" Text='<%#Eval("QD_YLBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_SYBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_SYBX" runat="server" Text='<%#Eval("QD_SYBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_YiLiaoBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_YiLiaoBX" runat="server" Text='<%#Eval("QD_YiLiaoBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_DEJZ" runat="server" align="center">
                                            <asp:Label ID="lbQD_DEJZ" runat="server" Text='<%#Eval("QD_DEJZ")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BuBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_BuBX" runat="server" Text='<%#Eval("QD_BuBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_GJJ" runat="server" align="center">
                                            <asp:Label ID="lbQD_GJJ" runat="server" Text='<%#Eval("QD_GJJ")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BGJJ" runat="server" align="center">
                                            <asp:Label ID="lbQD_BGJJ" runat="server" Text='<%#Eval("QD_BGJJ")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        
                                        <td align="center">
                                            <asp:Label ID="lbQD_ShuiDian" runat="server" Text='<%#Eval("QD_ShuiDian")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GeShui" runat="server" Text='<%#Eval("QD_GeShui")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_DaiKouXJ" runat="server" Text='<%#Eval("QD_DaiKouXJ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_ShiFaJE" runat="server" Text='<%#Eval("QD_ShiFaJE")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_KOUSJS" runat="server" Text='<%#Eval("QD_KOUSJS")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                        <tr>
                            <td colspan="7" align="right">
                            合计:
                            </td>
                            <td id="tdfoot1" colspan="2" runat="server" >
                            </td>
                            <td>  
                            </td>
                            <td id="tdfoot2" colspan="7" runat="server">  
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_JCGZhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_GZGLhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_GDGZhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_JXGZhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_JiangLihj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_BingJiaGZhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_JiaBanGZhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_BFJBhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_ZYBFhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_BFZYBhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_NianJiaGZhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_YKGWhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_TZBFhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_TZBKhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_JTBThj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_FSJWhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_CLBThj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_QTFYhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_YFHJhj" runat="server"></asp:Label>
                            </td>
                            
                            
                            
                            <td id="tdQD_YLBXhj" runat="server">
                                <asp:Label ID="lb_QD_YLBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_SYBXhj" runat="server">
                                <asp:Label ID="lb_QD_SYBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_YiLiaoBXhj" runat="server">
                                <asp:Label ID="lb_QD_YiLiaoBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_DEJZhj" runat="server">
                                <asp:Label ID="lb_QD_DEJZhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_BuBXhj" runat="server">
                                <asp:Label ID="lb_QD_BuBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_GJJhj" runat="server">
                                <asp:Label ID="lb_QD_GJJhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_BGJJhj" runat="server">
                                <asp:Label ID="lb_QD_BGJJhj" runat="server"></asp:Label>
                            </td>
                            
                            
                            
                            <td>
                                <asp:Label ID="lb_QD_ShuiDianhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_GeShuihj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_DaiKouXJhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_ShiFaJEhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_KOUSJShj" runat="server"></asp:Label>
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
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblSHJS_OnSelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                        <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    工资清单
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
                                                <asp:TextBox ID="tbfqryj" runat="server" Height="42px" TextMode="MultiLine"
                                                    Width="100%" Enabled="false"></asp:TextBox>
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
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server" Height="20px">
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
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Enabled="false" Height="20px">
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
                            
                            <tr id="sjshh" runat="server" visible="false">
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
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelPersons3()" Visible="false">
                                                    <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px" Enabled="false">
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
                                                <asp:TextBox ID="opinion3" runat="server" TextMode="MultiLine" Width="100%"
                                                Height="42px" Enabled="false"></asp:TextBox>
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
                                <input id="dep" name="dept"/>
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
