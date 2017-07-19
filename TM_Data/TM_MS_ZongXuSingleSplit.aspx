<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_ZongXuSingleSplit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_ZongXuSingleSplit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<base target="_self" />
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/MS_Adjust.js" type="text/javascript" charset="GB2312"></script>

    <title>������</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table>
                    <tr>
                    <td><strong>ע�⣺��ֺ��ܺϲ�������</strong></td>
                    </tr>
                 </table>
             </div>
         </div>
     </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width:100%;">
          <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <tr>
              <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;������Ҫ��������<strong style="color:Red;">���</strong>:</td>
              <td>
                  <asp:TextBox ID="txtZongXu" runat="server"></asp:TextBox>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnQuery" runat="server" Text="�� ѯ" OnClientClick="return CheckConfirmExist();" OnClick="btnQuery_OnClick" />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblPartWeight" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Label ID="lblTuPartWeight" runat="server" ForeColor="Red" Text=""></asp:Label>
                  </td>
            </tr>
            <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="grvOrgData" runat="server" Width="1200px" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="���" DataField="BM_XUHAO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="ͼ��(��ʶ��)" DataField="BM_TUHAO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="����" DataField="BM_ZONGXU" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="��������" DataField="BM_CHANAME" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="���ϱ���" DataField="BM_MARID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="���" DataField="BM_MAGUIGE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="����" DataField="BM_MAQUALITY" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="����" DataField="BM_NUMBER" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="ʵ�ʵ���(kg)" DataFormatString="{0:f2}" DataField="BM_UNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="ʵ������(kg)" DataFormatString="{0:f2}" DataField="BM_TOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="���ϵ���(kg)" DataFormatString="{0:f2}" DataField="BM_MAUNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="��������(kg)" DataFormatString="{0:f2}" DataField="BM_MATOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="����(mm)" DataFormatString="{0:f2}" DataField="BM_MALENGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="���(mm)" DataFormatString="{0:f2}" DataField="BM_MAWIDTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="�ܳ�(mm)" DataFormatString="{0:f2}" DataField="BM_MATOTALLGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="����(m&sup2;)" DataFormatString="{0:f3}" DataField="BM_MABGZMY" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_STANDARD" HeaderText="����" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_PROCESS" HeaderText="��������" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_KEYCOMS" HeaderText="�ؼ�����" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_ISMANU" HeaderText="������ϸ" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_NOTE" HeaderText="��ע" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_MANAME" HeaderText="��������" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_THRYWGHT" HeaderText="��������" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="BM_MASHAPE" HeaderText="ë����״" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="�Ƿ񶨳�" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                      <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                     <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                     <EditRowStyle BackColor="#2461BF" />
                     <AlternatingRowStyle BackColor="White" /> 
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">û�м�¼��
                </asp:Panel>
                <br />
            </td>
            </tr>
            <tr>
             <td align="right">��ֺ�����:</td>
             <td>
             <asp:TextBox ID="txtShuMu" runat="server" onblur="NumCheck(this);" Width="50px"></asp:TextBox>
             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckNum();" OnClick="btnAdd_OnClick" Text="����" />
             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" OnClientClick="return CheckDelete();" OnClick="btnDelete_OnClick" Text="ɾ��" />
             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnConfirm" runat="server" OnClientClick="return confirm('ȷ�ϲ����\r\r��ʾ��\r\r��ֺ�ԭ��¼ɾ�������ܺϲ�������');" OnClick="btnConfirm_OnClick" Text="����" />
             </td>
            </tr>
            <tr>
            <td colspan="2" align="center">
             <asp:Panel ID="NoDataPanelSplit" HorizontalAlign="Center" runat="server">û�м�¼��
                </asp:Panel>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="4" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                      <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" onclick="SelectOne(this);" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="���" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Index" runat="server" style="border-style:solid; width:70px"
                                 type="text" value='<%#Eval("BM_XUHAO") %>' />
                        </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ͼ��(��ʶ��)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtTuHao" runat="server" style="border-style:solid; width:200px" Text='<%#Eval("BM_TUHAO") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="����" DataField="BM_ZONGXU" ItemStyle-HorizontalAlign="Center" />
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="��������">
                          <ItemTemplate>
                              <asp:TextBox ID="txtChaName" runat="server" style="border-style:solid; width:100px" Text='<%#Eval("BM_CHANAME") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="���ϱ���" DataField="BM_MARID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="���Ϲ��" DataField="BM_MAGUIGE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="����" DataField="BM_MAQUALITY" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Num" runat="server" style="border-style:solid; width:30px" title="��������Ϊ����1������������"
                              onchange="CaculateTotalWeight(this)" type="text" value='<%#Eval("BM_NUMBER") %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ʵ�ʵ���(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="danzhong" runat="server" style="border-style:solid; width:70px"  onchange="CaculateTotalWeight(this);"
                                    type="text" title="�����ֵ��ǲ���,�˴���д���ǲ�������,��������������������" value='<%#Eval("BM_UNITWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="ʵ������(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="zongzhong" runat="server" title="�����ֵ��ǲ���,�˴���д���ǲ�������,��������������������" onchange="FloatCheck(this);" style=" color:Gray; width:70px" 
                                    type="text" value='<%#Eval("BM_TOTALWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="���ϵ���(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="cailiaodanzhong" runat="server"  style="border-style:solid; width:70px"  onchange="CaculateTotalWeight(this);"
                                    type="text"  value='<%#Eval("BM_MAUNITWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="��������(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="cailiaozongzhong" runat="server" style="color:Gray;width:70px"  onchange="FloatCheck(this);"
                                    type="text"  value='<%#Eval("BM_MATOTALWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="ͼֽ�ϵ���(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="tuzidz" runat="server" style="border-style:solid; width:70px"  onchange="FloatCheck(this);"
                                    type="text"  value='<%#Eval("BM_TUUNITWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="����(mm)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtLength" runat="server" style="border-style:solid;width:70px" onchange="CaculateTotalWeight(this);"  Text='<%#Eval("BM_MALENGTH") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="���(mm)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtWidth" runat="server" style="border-style:solid; width:70px" onchange="CaculateTotalWeight(this);"  Text='<%#Eval("BM_MAWIDTH") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="�����ܳ�(mm)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtTotalWidth" runat="server" style="border-style:solid; width:70px" onchange="CaculateTotalWeight(this);" Text='<%#Eval("BM_MATOTALLGTH") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="����(m&sup2;)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtBGZMY" runat="server" style="border-style:solid; width:70px" onchange="CaculateTotalWeight(this);" Text='<%#Eval("BM_MABGZMY") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="��ע">
                          <ItemTemplate>
                              <asp:TextBox ID="txtBZ" runat="server" style="border-style:solid; width:200px"  Text='<%#Eval("BM_NOTE") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BM_STANDARD" HeaderText="����" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_PROCESS" HeaderText="��������" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_KEYCOMS" HeaderText="�ؼ�����" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_ISMANU" HeaderText="������ϸ" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                     <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header" TableHeight="300px" TableWidth="1200px" FixColumns="0,1" />
            </yyc:SmartGridView>
              </td>
            </tr>
          </table>
          <input id="hdfDeleteRow" runat="server" type="hidden" />
        <br /> 
        <div style="font-size:medium;">��ʾ:<br />(1)����ֲ���ʱ��Ҫ��֤���ǰ�󡾲������ء���ͼֽ��������һ�£�<br />(2)���������ʱ��Ҫ��֤���ǰ��ʵ����������������������ͼֽ�������������򡿡������ܳ���һ�£�</div>
        
        </div>
    </div>
    </form>
</body>
</html>

