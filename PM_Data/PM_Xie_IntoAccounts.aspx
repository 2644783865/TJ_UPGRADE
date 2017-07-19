<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"  CodeBehind="PM_Xie_IntoAccounts.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_IntoAccounts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    编辑
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
        <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
        <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/Datetime.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/copy.js" type="text/javascript" charset="GB2312" language="javascript"></script>
     <script language="javascript" type="text/javascript">
         function document.onreadystatechange() {
             if (document.readyState == "complete") {
                 var ptc = "<%=gloabptc%>";
                 var col = 2;
                 othptcfind(ptc, col)
             }
         }
         function getClientId() {
             var paraId1 = '<%= LabelDate.ClientID %>';
             return { Id1: paraId1 };
         }
    </script>
 <div class="RightContent">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        外协结算单
                                    </td>
                                    <td align="right">
                                    <asp:Button ID="btn_delete" runat="server" Text="删除" ForeColor="Red" OnClick="btn_delete_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="confirm" runat="server" Text="保存" OnClick="save_Click" />&nbsp;&nbsp;&nbsp;
                                       <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_Click"  />&nbsp;&nbsp;
                                      <%--   <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_Click" Visible="false" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                        <asp:Button ID="goback" runat="server" Text="返回" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                 <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="4">
                                        外协结算单
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20%;" align="left" >
                                    &nbsp;&nbsp;&nbsp;制单人：&nbsp;&nbsp;&nbsp;<asp:Label ID="lab_zdr" runat="server" ></asp:Label>
                                    </td>
                                    <td style="width: 40%;" align="left">
                                        &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 60%;" colspan="3">
                                        &nbsp;&nbsp;&nbsp;结算单备注：
                                        <asp:TextBox ID="Tb_note" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                 <div style="border: 1px solid #000000; height: 360px;">
                            <div class="cpbox7 xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="PurorderdetailRepeater" runat="server"  >
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                                 <td>
                                                    <strong>加工单号</strong>
                                                </td>
                                                <td>
                                                    <strong>图号</strong>
                                                </td>
                                                 <td>
                                                    <strong>名称</strong>
                                                </td>
                                                <td>
                                                 <strong>材料</strong>
                                                </td>
                                                <td>
                                                  <strong>规格</strong>
                                                </td>
                                                <td>
                                                <strong>数量</strong>
                                                </td>
                                                 <td>
                                                    <strong>结算数量</strong>
                                                </td>
                                                 <td>
                                                    <strong>单重</strong>
                                                </td>
                                                 <td>
                                                    <strong>总重</strong>
                                                </td>
                                                <td>
                                                    <strong>外协类型</strong>
                                                </td>
                                                <td>
                                                    <strong>外协工序</strong>
                                                </td>
                                                 <td>
                                                    <strong>单价</strong>
                                                </td>
                                                  <td>
                                                    <strong>金额</strong>
                                                  </td>
                                                <td>
                                                    <strong>交货日期</strong>
                                                </td>
                                                   <td>
                                                    <strong>实际交货日期</strong>
                                                  </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="TA_ORDERNUM" runat="server" Visible="false" Text='<%#Eval("TA_ORDERNUM") %>'></asp:Label>
                                                </td>
                                                <td>
                                                <div style="width:200px;white-space:normal">
                                                    <asp:Label ID="TA_PTC" runat="server" Text='<%#Eval("TA_PTC")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                   <td>
                                                    <asp:Label ID="PIC_JGNUM" runat="server" Text='<%#Eval("PIC_JGNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_TUHAO" runat="server" Text='<%#Eval("TA_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_MNAME" runat="server" Text='<%#Eval("TA_MNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_CAIZHI" runat="server" Text='<%#Eval("TA_CAIZHI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_GUIGE" runat="server" Text='<%#Eval("TA_GUIGE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                <asp:Label ID="TA_ZXNUM" runat="server" Text='<%#Eval("TA_ZXNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TA_NUM" runat="server" Text='<%#Eval("TA_NUM")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_UWGHT" runat="server" Text='<%#Eval("TA_UWGHT")%>'></asp:Label>
                                                </td>
                                                  <td>
                                                    <asp:Label ID="TA_WGHT" runat="server" Text='<%#Eval("TA_WGHT")%>'></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="TA_WXTYPE" runat="server" Text='<%#Eval("TA_WXTYPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_PROCESS" runat="server" Text='<%#Eval("TA_PROCESS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_PRICE" runat="server" Text='<%#Eval("TA_PRICE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_MONEY" runat="server" Text='<%#Eval("TA_MONEY")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_JHQ" runat="server" Text='<%#Eval("TA_JHQ")%>'></asp:Label>
                                                </td>
                                               <td>
                                                    <asp:Label ID="BJSJ" runat="server" Text='<%#Eval("BJSJ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TA_NOTE" runat="server" Text='<%#Eval("TA_NOTE")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="17" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
 <script language="javascript" type="text/javascript">
     //                                    var myST = new superTable("tab", {
     //                                        cssSkin: "tDefault",
     //                                        headerRows: 1,
     //                                        fixedCols: 0,
     //                                        //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
     //                                        onStart: function() {
     //                                            //		        this.start = new Date();
     //                                        },
     //                                        onFinish: function() {
     //                                            //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
     //                                        }
     //                                    });
                                </script>

                            </div>
                        </div>
                    </div>
                </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
              </asp:Content>



